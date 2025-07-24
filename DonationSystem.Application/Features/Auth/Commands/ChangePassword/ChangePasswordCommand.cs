using DonationSystem.Application.DTOs.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, ChangePasswordDto Dto) : IRequest<string>;
}
