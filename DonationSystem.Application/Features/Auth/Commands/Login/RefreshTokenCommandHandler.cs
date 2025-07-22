using DonationSystem.Application.DTOs;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Domain.Specifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Login
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public RefreshTokenCommandHandler(IUnitOfWork uow, IAuthService authService)
        {
            _unitOfWork = uow;
            _authService = authService;
        }

        public async Task<LoginResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var user = await userRepo.GetEntityWithSpecAsync(new UserByRefreshTokenSpecification(request.RefreshToken));

            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                throw new Exception("Invalid or expired refresh token");

            var jwt = _authService.GenerateJwtToken(user);
            string refreshToken = _authService.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.CompleteAsync();
            return new LoginResponseDto
            {
                Token = jwt,
                RefreshToken = refreshToken
            };
        }
    }
}
