using AccountingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}




