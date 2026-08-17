using AccountingSystem.Application.DTOs.Register;
using AccountingSystem.Application.Exceptions;
using AccountingSystem.Application.Interfaces.Auth;
using AccountingSystem.Application.Interfaces.Repositories.UserRepository;
using AccountingSystem.Application.Interfaces.Services;
using AccountingSystem.Application.Interfaces.UOW;
using AccountingSystem.Domain.Entities;
using FluentValidation;
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
        private readonly IValidationService _validationService;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IUnitOfWork unitOfWork,
            IValidationService validationService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _validationService= validationService;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            await _validationService.ValidateAsync(request);

            if (await _userRepository.UserNameExistsAsync(request.UserName))
            {
               throw new ConflictException("Username already exists.");
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
