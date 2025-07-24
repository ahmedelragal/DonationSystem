using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.DTOs
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
    }
}
