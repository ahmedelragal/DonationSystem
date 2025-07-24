using AutoMapper;
using DonationSystem.Application.DTOs.Auth;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public LoginCommandHandler(IUnitOfWork uow, IAuthService authService)
        {
            _unitOfWork = uow;
            _authService = authService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var spec = new UserByEmailSpecification(request.Email);
            var user = await userRepo.GetEntityWithSpecAsync(spec);

            if (user == null || !_authService.VerifyPassword(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");


            var jwt = _authService.GenerateJwtToken(user);
            string refreshToken = _authService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.CompleteAsync();
            return new LoginResponseDto
            {
                UserId = user.Id,
                Token = jwt,
                RefreshToken = refreshToken
            };
        }
    }
}
