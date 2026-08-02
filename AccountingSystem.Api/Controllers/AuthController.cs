using AccountingSystem.Application.DTOs.Register;
using AccountingSystem.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);


            if (result.Message.Contains("already"))
            {
                return Conflict(result); // 409
            }


            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}
