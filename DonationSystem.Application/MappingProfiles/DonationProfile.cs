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
    public class DonationProfile : Profile
    {
        public DonationProfile()
        {
            CreateMap<CreateDonationDto, Donation>();
            CreateMap<Donation, DonationDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}
