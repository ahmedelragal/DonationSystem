using DonationSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommand : IRequest<string>
    {
        public RegisterUserDto UserDto { get; set; }

        public RegisterUserCommand(RegisterUserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
