using AutoMapper;
using DonationSystem.Application.DTOs;
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
    public class GetDonationsHandler : IRequestHandler<GetDonationsQuery, List<DonationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDonationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DonationDto>> Handle(GetDonationsQuery request, CancellationToken cancellationToken)
        {
            var spec = new DonationWithUserSpecification();
            var donations = await _unitOfWork.Repository<Donation>().ListAsync(spec);
            return _mapper.Map<List<DonationDto>>(donations);
        }
    }
}
