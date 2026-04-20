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
    }
}
