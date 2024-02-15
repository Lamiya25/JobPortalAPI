using JobPortalAPI.Application.Abstractions.IServices.Infrastructure.TokenServices;
using JobPortalAPI.Infrastructure.Implementation.Services.TokenServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Infrastructure.Registration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection service)
        {
            service.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
