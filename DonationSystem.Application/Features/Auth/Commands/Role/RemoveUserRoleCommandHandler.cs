using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Role
{
    public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var spec = new UserWithRolesSpecification(request.UserId);
            var user = await userRepo.GetEntityWithSpecAsync(spec);

            if (user == null)
                throw new Exception("User not found");

            var userRole = user.UserRoles.FirstOrDefault(r => r.RoleName == request.RoleName);
            if (userRole == null)
                return "User does not have this role";

            user.UserRoles.Remove(userRole);

            userRepo.Update(user);
            await _unitOfWork.CompleteAsync();

            return "Role removed successfully";
        }
    }
}
