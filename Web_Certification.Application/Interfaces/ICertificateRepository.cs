using System;
using System.Collections.Generic;
using System.Text;
using Web_Certification.Domain.Entities;

namespace Web_Certification.Application.Interfaces
{
    public interface ICertificateRepository
    {
            Task<Certificate> AddAsync(Certificate certificate);
            Task<Certificate?> GetByHashAsync(string certHash);
            Task<IEnumerable<Certificate>> GetCertificatesAsync(
                string? name,
                string? documentType,
                DateTime? issueDate,
                DateTime? expirationDate,
                bool? status,
                string? studentWallet,
                string? className,
                string? department,
                string? specialization,
                bool? isPermanent);
            Task<Certificate?> GetByIdAsync(int id);
            Task UpdateAsync(Certificate certificate);
        }
    }
