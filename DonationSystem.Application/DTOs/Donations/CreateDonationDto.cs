using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.DTOs.Donations
{
    public class CreateDonationDto
    {
        public string? Title { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public List<IFormFile> Images { get; set; } = new();
    }
}
