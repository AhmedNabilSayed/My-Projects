#nullable enable
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TourismPlaces.Helper
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);
            var fileName =$"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }

        public static void DeleteFile(string removedFileName, string folderName)
        {
            //var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            //var filePath = Path.Combine(folderPath, removedFileName);

            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}

            if (!string.IsNullOrEmpty(removedFileName))
            {
                var path = Directory.GetCurrentDirectory() + "/wwwroot/Files/" + Path.Combine(folderName, removedFileName);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public static string GetDirectoryPath(string folderName)
        {
           return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

        }
    }
}
