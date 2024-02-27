using JobPortalAPI.Application.Abstractions.IServices.Infrastructure.TokenServices;
using JobPortalAPI.Application.Abstractions.IServices.Persistance.IStorage;
using JobPortalAPI.Infrastructure.Implementation.Services.Storage;
using JobPortalAPI.Infrastructure.Implementation.Services.TokenServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static JobPortalAPI.Infrastructure.Implementation.Services.Storage.Storage;

namespace JobPortalAPI.Infrastructure.Registration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection service)
        {
            service.AddScoped<ITokenHandler, TokenHandler>();
            service.AddScoped<IStorageService, StorageService>();
            service.AddScoped<IStorage, LocalStorage>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
    }
}
