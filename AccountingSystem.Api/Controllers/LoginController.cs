using AccountingSystem.Application.DTOs.Login;
using AccountingSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController:ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var result = await _loginService.LoginAsync(dto);

            return Ok(result);
        }
    }
}
