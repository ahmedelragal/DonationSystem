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
            CreateMap<CreateDonationDto, Donation>()
           .ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<Donation, DonationDto>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
             .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl).ToList()));
        }
    }
}
