using DonationSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Login
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResponseDto>;
}
