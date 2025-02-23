using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLayer.Application.Helpers.HttpResponseExceptions;
using TestLayer.Core.Interfaces.Services;
using TestLayer.Infrastructure.Data;

namespace TestLayer.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IUserService _userService;

        public FileService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task UploadFileAsync(Stream fileStream, string fileName, string uploadsFolder)
        {
            if (string.IsNullOrEmpty(uploadsFolder))
                throw new ArgumentNullException(nameof(uploadsFolder));

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, fileName);

            using (var outputStream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(outputStream);
            }
        }

        public void DeleteFile(string fileName, string uploadsFolder)
        {
            if (string.IsNullOrEmpty(uploadsFolder))
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "File path is wrong.");

            string filePath = Path.Combine(uploadsFolder, fileName);

            if (!File.Exists(filePath))
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "File not found");

            File.Delete(filePath);
        }

        /*
        public async Task UploadFileAsync(Stream fileStream, int userId, string webRootPath)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId)
                    ?? throw new CustomException(System.Net.HttpStatusCode.NotFound, $"User with  Id `{userId}` not found.");

                if (string.IsNullOrEmpty(webRootPath))
                    throw new CustomException(System.Net.HttpStatusCode.BadRequest, "File path is wrong.");

                string uploadsFolder = Path.Combine(webRootPath, "uploads", "ProfilePics", userId.ToString());

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string filePath = Path.Combine(uploadsFolder, "pp.png");

                using (var outputStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.CopyToAsync(outputStream);
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, $"An Internal error occured. {ex.Message}");
            }
        }

        public async Task DeleteFile(int userId, string webRootPath)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId)
                    ?? throw new CustomException(System.Net.HttpStatusCode.NotFound, $"User with  Id `{userId}` not found.");

                if (string.IsNullOrEmpty(webRootPath))
                    throw new CustomException(System.Net.HttpStatusCode.BadRequest, "File path is wrong.");

                string filePath = Path.Combine(webRootPath, "uploads", "ProfilePics", userId.ToString());

                if (File.Exists(filePath))
                    File.Delete(filePath);
                else
                    throw new CustomException(System.Net.HttpStatusCode.NotFound, $"File not found.");
            }
            catch (Exception ex)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, $"An Internal error occured. {ex.Message}");
            }


        }

        */
    }
}
