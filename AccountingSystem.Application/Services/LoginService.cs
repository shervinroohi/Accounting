using AccountingSystem.Application.DTOs.Login;
using AccountingSystem.Application.Interfaces;
using AccountingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Services
{
    public class LoginService : ILoginService
    {


        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginService(
            IUserRepository userRepository,
            ITokenService tokenService,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByUserNameAsync(dto.UserName);

            if (user == null)
                throw new Exception("Username or password is incorrect.");

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

  
            if (verificationResult == PasswordVerificationResult.Failed)
                throw new Exception("Username or password is incorrect.");

            var token = _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}
