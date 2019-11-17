using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CargoWorld.Data.FileManager
{
    public class FIleManager : IFileManager
    {
        private string imagePath;

        public FIleManager(IConfiguration configuration)
        {
            imagePath = configuration["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            var savePath = Path.Combine(imagePath);
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            var type = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{type}";

            using (var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
            {
              await  file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
