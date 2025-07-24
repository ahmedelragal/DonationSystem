using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<string> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var user = await userRepo.GetByIdAsync(request.UserId);

            if (user is null)
                throw new Exception("User not found.");
            
            if (request.Dto.NewPassword != request.Dto.ConfirmPassword)
                throw new Exception("New password and confirmation do not match");

            if (!_authService.VerifyPassword(request.Dto.CurrentPassword, user.PasswordHash))
                throw new Exception("Current password is incorrect.");

            user.PasswordHash = _authService.HashPassword(request.Dto.NewPassword);

            userRepo.Update(user);
            await _unitOfWork.CompleteAsync();

            return "Password changed successfully.";
        }
    }
}
