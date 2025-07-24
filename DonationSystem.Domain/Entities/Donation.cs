using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Entities
{
    public class Donation
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public required User User { get; set; }
        public ICollection<DonationImage> Images { get; set; } = new List<DonationImage>();
    }
}
