using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Certification.Application.Interfaces
{
    public interface IBlobStorageService
    {
        // Nhận file từ FE, trả về URL của file trên Azure
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
    }
}
