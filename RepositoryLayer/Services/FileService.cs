using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class FileService
    {
        private IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task<Tuple<int, string>> SaveImage(IFormFile imagefile)
        {
            try
            {
                var connectPath = this._webHostEnvironment.ContentRootPath;
                var path = Path.Combine(connectPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var Extension = Path.GetExtension(imagefile.FileName);
                var AllowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!AllowedExtensions.Contains(Extension))
                {
                    string message = string.Format("Only {0} extensions are allowed", string.Join(", ", AllowedExtensions));
                    return new Tuple<int, string>(0, message);
                }
                string uniquestring = Guid.NewGuid().ToString();
                string newFilename = uniquestring + Extension;
                string fullPath = Path.Combine(path, newFilename);
                using (var filestream = new FileStream(fullPath, FileMode.Create))
                {
                    await imagefile.CopyToAsync(filestream);
                }
                return new Tuple<int, string>(1, "Image Saved Sucessfully");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
