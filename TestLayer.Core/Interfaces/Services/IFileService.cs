using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Core.Interfaces.Services
{
    public interface IFileService
    {
        Task UploadFileAsync(Stream fileStream, string fileName, string uploadsFolder);
        void DeleteFile(string fileName, string uploadsFolder);
    }
}
