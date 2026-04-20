using Microsoft.AspNetCore.Http;
using System;

namespace Web_Certification.Application.DTOs
{
    // DTO hứng dữ liệu từ FormData của Next.js
    public class IssueCertificateDto
    {
        public string? StudentName { get; set; }
        public string? StudentWallet { get; set; }
        public string? CertHash { get; set; }
        public string? DocumentType { get; set; }

        public string? Title { get; set; }
        public string? ClassName { get; set; }
        public string? Department { get; set; }
        public string? Content { get; set; }
        public string? Location { get; set; }

        // Các field dành riêng cho Bằng tốt nghiệp
        public string? DegreeType { get; set; }
        public string? Specialization { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GraduationYear { get; set; }
        public string? Classification { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsPermanent { get; set; }

        public long? ExpirationTimestamp { get; set; }
        public IFormFile? File { get; set; } // File ảnh
    }
}
