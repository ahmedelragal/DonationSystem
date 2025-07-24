using AutoMapper;
using DonationSystem.Application.DTOs.Donations;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Queries.GetDonations
{
    public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, DonationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDonationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DonationDto> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new DonationWithUserImageSpecifiaction(request.Id);
            var donation = await _unitOfWork.Repository<Donation>().GetEntityWithSpecAsync(spec);

            if (donation == null)
                throw new Exception("Donation Not Found");

            return _mapper.Map<DonationDto>(donation);
        }

    }
}
