using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace JobPortalAPI.Infrastructure.Implementation.Services.Storage
{
    public partial class Storage
    {
        public class LocalStorage : Storage, ILocalStorage
        {
            private readonly IWebHostEnvironment _webHostEnvironment;

            public LocalStorage(IWebHostEnvironment webHostEnvironment)
            {
                _webHostEnvironment = webHostEnvironment;
            }

            public Task DeleteAsync(string pathName, string fileName)
            {
                throw new NotImplementedException();
            }

            public List<string> GetFiles(string pathName)
            {
                throw new NotImplementedException();
            }

            public bool HasFile(string pathName, string fileName)
            {
                throw new NotImplementedException();
            }

            public async Task<List<(string fileName, string pathName)>> UploadAsync(string pathName, IFormFileCollection files)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathName);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                List<(string fileName, string path)> values = new();

                foreach (IFormFile file in files)
                {
                    string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                    await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
                    values.Add((fileNewName, Path.Combine(pathName, fileNewName)));
                }
                return values;
            }

            public async Task<(string fileName, string pathName)> UploadAsync(string pathName, IFormFile file)
            {
                string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathName);

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
                return (fileNewName, Path.Combine(pathName, fileNewName));
            }

            private async Task<bool> CopyFileAsync(string path, IFormFile file)
            {
                try
                {
                    await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error copying file", ex);
                }
            }
        }
    }
}
