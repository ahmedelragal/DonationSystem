using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();

            var user = await userRepo.GetByIdAsync(request.UserId);
            if (user == null)
                return false;

            user.FullName = request.Dto.FullName!;
            user.Email = request.Dto.Email!;

            userRepo.Update(user);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
