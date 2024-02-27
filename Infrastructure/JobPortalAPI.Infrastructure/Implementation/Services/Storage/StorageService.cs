using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage.Local;
using JobPortalAPI.Infrastructure.Implementation.Services.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Infrastructure.Implementation.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;
        public StorageService(IStorage storage) => _storage = storage;


        public string StorageName { get => _storage.GetType().Name; }

        public Task DeleteAsync(string pathName, string fileName) => _storage.DeleteAsync(pathName, fileName);

        public List<string> GetFiles(string pathName) => _storage.GetFiles(pathName);

        public bool HasFile(string pathName, string fileName) => _storage.HasFile(pathName, fileName);

        public Task<List<(string fileName, string pathName)>> UploadAsync(string pathName, IFormFileCollection files)
               => _storage.UploadAsync(pathName, files);

        public Task<(string fileName, string pathName)> UploadAsync(string pathName, IFormFile file)
            => _storage.UploadAsync(pathName, file);
    }
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string pathName, string fileName)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                DateTime datetimenow = DateTime.UtcNow;
                string datetimeutcnow = datetimenow.ToString("yyyyMMddHHmmss");
                string newFileName = $"{datetimeutcnow}{NameOperation.CharacterRegulatory(oldName)}{extension}";

                if (File.Exists($"{pathName}\\{newFileName}"))
                    return await FileRenameAsync("", newFileName);
                else
                    return newFileName;
            });
            return newFileName;
        }
        /* public class LocalStorage : Storage, ILocalStorage
         {
             private readonly IWebHostEnvironment _webHostEnvironment;
             public LocalStorage(IWebHostEnvironment webHostEnvironment)
             {
                 _webHostEnvironment = webHostEnvironment;
             }

             public async Task DeleteAsync(string pathName, string fileName)
                     => File.Delete($"{pathName}\\{fileName}");

             public List<string> GetFiles(string pathName)
             {
                 DirectoryInfo directory = new(pathName);
                 return directory.GetFiles().Select(f => f.Name).ToList();
             }

             public bool HasFile(string pathName, string fileName)
                 => File.Exists($"{pathName}\\{fileName}");

             public async Task<List<(string fileName, string pathName)>> UploadAsync(string pathName, IFormFileCollection files)
             {
                 string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathName);

                 if (!Directory.Exists(uploadPath))
                     Directory.CreateDirectory(uploadPath);

                 List<(string fileName, string path)> values = new();

                 foreach (IFormFile file in files)
                 {
                     string fileNewName = await FileRenameAsync(pathName, file.FileName);
                     await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                     values.Add((fileNewName, $"{pathName}\\{fileNewName}"));
                 }
                 return values;
             }

             public async Task<(string fileName, string pathName)> UploadAsync(string pathName, IFormFile file)
             {
                 string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, pathName);

                 if (!Directory.Exists(uploadPath))
                     Directory.CreateDirectory(uploadPath);

                 (string fileName, string path) values = new();

                 string fileNewName = await FileRenameAsync(pathName, file.FileName);
                 await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                 values.fileName = fileNewName;
                 values.path = $"{pathName}\\{fileNewName}";
                 return values;
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
                     //todo log!
                     throw ex;
                 }
             }*/

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
                    // Log the exception for debugging
                    throw new Exception("Error copying file", ex);
                }
            }
        }
    }
}
