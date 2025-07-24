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
    public class AddUserRoleCommandHandler : IRequestHandler<AddUserRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUserRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var spec = new UserWithRolesSpecification(request.UserId);
            var user = await userRepo.GetEntityWithSpecAsync(spec);
            if (user == null)
                throw new Exception("User not found");

            // Check if user already has the role
            var hasRole = user.UserRoles.Any(r => r.RoleName == request.RoleName);
            if (hasRole)
                return "User already has this role";

            // Add new role
            user.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleName = request.RoleName
            });

            userRepo.Update(user);
            await _unitOfWork.CompleteAsync();

            return "Role added successfully";
        }
    }
}
