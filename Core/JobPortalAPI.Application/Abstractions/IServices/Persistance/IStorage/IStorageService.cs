namespace JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
    }
}
