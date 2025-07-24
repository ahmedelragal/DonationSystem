using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.DTOs.Reports
{
    public class DateFilteredDonationReportDto
    {
        public List<DonationDto> Donations { get; set; } = new();
        public int TotalDonations { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
