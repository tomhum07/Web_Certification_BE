using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Certification.Application.Interfaces;
using Web_Certification.Domain.Entities;

namespace Web_Certification.Infrastructure.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly AppDbContext _context;

        public CertificateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Certificate> AddAsync(Certificate certificate)
        {
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();
            return certificate;
        }

        public async Task<Certificate?> GetByHashAsync(string certHash)
        {
            return await _context.Certificates.FirstOrDefaultAsync(c => c.CertHash == certHash);
        }

        public async Task<System.Collections.Generic.IEnumerable<Certificate>> GetCertificatesAsync(
            string? name,
            string? documentType,
            System.DateTime? issueDate,
            System.DateTime? expirationDate,
            bool? status,
            string? studentWallet,
            string? className,
            string? department,
            string? specialization,
            bool? isPermanent)
        {
            var query = _context.Certificates.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.StudentName.Contains(name));

            if (!string.IsNullOrEmpty(documentType))
                query = query.Where(c => c.DocumentType == documentType);

            if (issueDate.HasValue)
                query = query.Where(c => c.IssueDate.HasValue && c.IssueDate.Value.Date == issueDate.Value.Date);

            if (expirationDate.HasValue)
                query = query.Where(c => c.ExpirationDate.HasValue && c.ExpirationDate.Value.Date == expirationDate.Value.Date);

            if (status.HasValue)
                query = query.Where(c => c.Status == status.Value);

            if (!string.IsNullOrEmpty(studentWallet))
                query = query.Where(c => c.StudentWallet == studentWallet);

            if (!string.IsNullOrEmpty(className))
                query = query.Where(c => c.ClassName == className);

            if (!string.IsNullOrEmpty(department))
                query = query.Where(c => c.Department == department);

            if (!string.IsNullOrEmpty(specialization))
                query = query.Where(c => c.Specialization == specialization);

            if (isPermanent.HasValue)
                query = query.Where(c => c.IsPermanent == isPermanent.Value);

            return await query.ToListAsync();
        }

        public async Task<Certificate?> GetByIdAsync(int id)
        {
            return await _context.Certificates.FindAsync(id);
        }

        public async Task UpdateAsync(Certificate certificate)
        {
            _context.Certificates.Update(certificate);
            await _context.SaveChangesAsync();
        }
    }
}
