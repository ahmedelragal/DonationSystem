using AutoMapper;
using DonationSystem.Application.DTOs;
using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateDonationDto, Donation>();
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
