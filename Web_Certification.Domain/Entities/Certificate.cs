using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Certification.Domain.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentWallet { get; set; } = string.Empty;
        public string CertHash { get; set; } = string.Empty; // Mã Hash lưu trên Blockchain
        public string FileUrl { get; set; } = string.Empty;  // Link ảnh trên Azure Blob
        public string DocumentType { get; set; } = string.Empty;

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
        public bool IsPermanent { get; set; }
        public bool Status { get; set; } // false hoặc true (tương ứng 0 hoặc 1)
    }
}
