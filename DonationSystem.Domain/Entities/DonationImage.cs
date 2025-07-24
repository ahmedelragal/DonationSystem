using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Entities
{
    public class DonationImage
    {
        public Guid Id{ get; set; }
        public string? ImageUrl { get; set; }
        public Guid DonationId { get; set; }
        public Donation? Donation { get; set; }

    }
}
