using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Role
{
    public record AddUserRoleCommand(Guid UserId, string RoleName) : IRequest<string>;
}
