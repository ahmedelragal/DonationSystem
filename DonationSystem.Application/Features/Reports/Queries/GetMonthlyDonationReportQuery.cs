using DonationSystem.Application.DTOs.Reports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Reports.Queries
{
    public record GetMonthlyDonationReportQuery(int Year) : IRequest<List<MonthlyDonationReportDto>>;
}
