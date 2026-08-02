using AccountingSystem.Application.DTOs.Register;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Repositories;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {

            if (await _userRepository.UserNameExistsAsync(request.UserName))
            {
                return new RegisterResponseDto
                {
                    Message = "This username is already taken."
                };
            }

            var user = new User
            {
                UserName = request.UserName
            };


            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            await _userRepository.AddAsync(user);

            await _unitOfWork.SaveChangesAsync();

            return new RegisterResponseDto
            {
                Message = "Registration was successful."
            };
        }
    }
}
