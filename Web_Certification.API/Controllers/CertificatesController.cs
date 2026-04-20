using Microsoft.AspNetCore.Mvc;
using Web_Certification.Application.DTOs;
using Web_Certification.Application.Interfaces;
using Web_Certification.Domain.Entities;

namespace Web_Certification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateRepository _repository;
        private readonly IBlobStorageService _blobService;

        public CertificatesController(ICertificateRepository repository, IBlobStorageService blobService)
        {
            _repository = repository;
            _blobService = blobService;
        }

        [HttpPost]
        public async Task<IActionResult> IssueCertificate([FromForm] IssueCertificateDto request)
        {
            try
            {
                // Kiểm tra xem người dùng có truyền file lên không
                if (request.File == null || request.File.Length == 0)
                {
                    return BadRequest(new { Message = "Vui lòng đính kèm file ảnh chứng chỉ (File)." });
                }

                // 1. Lưu file ảnh lên Azure Blob Storage
                var fileExtension = Path.GetExtension(request.File.FileName);

                // Xác định thư mục dựa trên DocumentType
                string folder = request.DocumentType switch
                {
                    "Giấy khen" => "Giay_Khen",
                    "Giấy chứng nhận" => "Giay_Chung_Nhan",
                    "Bằng tốt nghiệp" => "Bang_Tot_Nghiep",
                    _ => "Khac"
                };

                // Đặt tên file theo tên của người được cấp (StudentName), nếu không có thì dùng CertHash
                var safeName = string.IsNullOrWhiteSpace(request.StudentName) ? request.CertHash : request.StudentName.Trim().Replace(" ", "_");
                var blobFileName = $"{folder}/{safeName}{fileExtension}";

                // --- ĐÂY LÀ ĐOẠN CODE CỦA CÁCH 2, PHẦN 2 ---
                // Mở file ra thành Stream
                using var fileStream = request.File.OpenReadStream();
                // Truyền Stream xuống service
                var fileUrl = await _blobService.UploadFileAsync(fileStream, blobFileName);
                // -------------------------------------------

                // Hàm hỗ trợ chuyển đổi Date sang giờ Việt Nam (UTC+7)
                DateTime? ToVietnamTime(DateTime? date) 
                {
                    if (!date.HasValue) return null;
                    // Nếu client gửi lên có định dạng múi giờ (UTC/Local), chuyển về UTC+7
                    // Nếu không (Unspecified), giữ nguyên giá trị client gửi
                    return date.Value.Kind == DateTimeKind.Unspecified 
                        ? date.Value 
                        : date.Value.ToUniversalTime().AddHours(7);
                }

                // 2. Tạo Entity và lưu vào Azure SQL
                var certificate = new Certificate
                {
                    StudentName = request.StudentName ?? string.Empty,
                    StudentWallet = request.StudentWallet ?? string.Empty,
                    CertHash = request.CertHash ?? string.Empty,
                    DocumentType = request.DocumentType ?? string.Empty,
                    FileUrl = fileUrl,
                    Title = request.Title,
                    ClassName = request.ClassName,
                    Department = request.Department,
                    Content = request.Content,
                    Location = request.Location,
                    DegreeType = request.DegreeType,
                    Specialization = request.Specialization,
                    DateOfBirth = ToVietnamTime(request.DateOfBirth),
                    GraduationYear = request.GraduationYear,
                    Classification = request.Classification,
                    IssueDate = ToVietnamTime(request.IssueDate) ?? DateTime.UtcNow.AddHours(7),
                    ExpirationDate = (request.IsPermanent ?? false) ? null : ToVietnamTime(request.ExpirationDate),
                    IsPermanent = request.IsPermanent ?? false
                };

                await _repository.AddAsync(certificate);

                return Ok(new { Message = "Lưu chứng chỉ thành công", FileUrl = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
