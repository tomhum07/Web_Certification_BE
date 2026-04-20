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
    }
}
