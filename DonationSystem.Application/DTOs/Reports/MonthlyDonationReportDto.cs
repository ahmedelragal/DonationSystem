using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.DTOs.Reports
{
    public class MonthlyDonationReportDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<DonationDto>? Donations { get; set; }
        public int TotalDonations { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
