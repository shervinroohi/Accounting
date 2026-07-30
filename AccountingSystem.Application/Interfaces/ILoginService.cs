using AccountingSystem.Application.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
