using AutoMapper;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userRepo = _unitOfWork.Repository<User>();
            var spec = new UserByEmailSpecification(request.UserDto.Email);
            var existing = await userRepo.GetEntityWithSpecAsync(spec);
            if (existing != null)
                throw new Exception("Email already registered");

            var user = _mapper.Map<User>(request.UserDto);
            user.PasswordHash = _authService.HashPassword(request.UserDto.Password);

            await userRepo.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return "User registered successfully";
        }
    }
}
