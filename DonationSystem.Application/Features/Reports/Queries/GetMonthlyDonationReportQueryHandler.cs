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
    public class GetMonthlyDonationReportQueryHandler : IRequestHandler<GetMonthlyDonationReportQuery, List<MonthlyDonationReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMonthlyDonationReportQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MonthlyDonationReportDto>> Handle(GetMonthlyDonationReportQuery request, CancellationToken cancellationToken)
        {
            var spec = new DonationByYearWithUserSpecification(request.Year);
            var donations = await _unitOfWork.Repository<Donation>().ListAsync(spec);

            var grouped = donations
                .GroupBy(d => new { d.DonatedAt.Year, d.DonatedAt.Month })
                .OrderBy(g => g.Key.Month)
                .Select(g => new MonthlyDonationReportDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalDonations = g.Count(),
                    TotalAmount = g.Sum(d => d.Amount),
                    Donations = _mapper.Map<List<DonationDto>>(g.ToList())
                })
                .ToList();

            return grouped;
        }
    }
}
