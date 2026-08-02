using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces.Auth
{
    public interface ICurrentUserService
    {
        int UserId { get; }

    }
}
