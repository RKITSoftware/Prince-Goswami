using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT_Token
{
    // Example of IAuthenticationService interface
    public interface IAuthenticationService
    {
        bool ValidateCredentials(string userName, string password);
        string GenerateToken(string userName);
    }
}