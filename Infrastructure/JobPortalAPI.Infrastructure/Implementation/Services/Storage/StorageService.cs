using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage;
using JobPortalAPI.Infrastructure.Implementation.Services.Operations;
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

       /* public Task<(string fileName, string pathName)> UploadAsync(string pathName, IFormFile file)
            => _storage.UploadAsync(pathName, file);*/

        public async Task<(string fileName, string pathName)> UploadAsync(string pathName, IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fullPath = Path.Combine(pathName, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return (fileName, fullPath);
        }

    }
    public partial class Storage
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


    }
}
