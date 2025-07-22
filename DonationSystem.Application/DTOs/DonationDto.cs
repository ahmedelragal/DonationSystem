using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.DTOs
{
    public class DonationDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
