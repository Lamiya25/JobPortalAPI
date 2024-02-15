using JobPortalAPI.Application.Abstractions.IServices.Persistance.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortalAPI.Application.Abstractions.IServices.Persistance.IUserServices
{
    public interface IAuthoService:IInternalAuthenticationService,IExternalAuthenticationService
    {
        
    }
}
