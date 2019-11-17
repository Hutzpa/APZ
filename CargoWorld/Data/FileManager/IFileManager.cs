using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoWorld.Data.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile file);

        FileStream ImageStream(string image);
    }
}
