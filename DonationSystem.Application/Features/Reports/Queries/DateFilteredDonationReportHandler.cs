using AutoMapper;
using DonationSystem.Application.DTOs.Donations;
using DonationSystem.Application.DTOs.Reports;
using DonationSystem.Domain.Entities;
using DonationSystem.Domain.Interfaces;
using DonationSystem.Domain.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Reports.Queries
{
    public class DateFilteredDonationReportHandler : IRequestHandler<DateFilteredDonationReportQuery, DateFilteredDonationReportDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DateFilteredDonationReportHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DateFilteredDonationReportDto> Handle(DateFilteredDonationReportQuery request, CancellationToken cancellationToken)
        {
            var spec = new DonationByDateRangeSpecification(request.From, request.To);
            var donations = await _unitOfWork.Repository<Donation>().ListAsync(spec);

            return new DateFilteredDonationReportDto
            {
                Donations = _mapper.Map<List<DonationDto>>(donations),
                TotalDonations = donations.Count,
                TotalAmount = donations.Sum(d => d.Amount)
            };
        }
    }
}
