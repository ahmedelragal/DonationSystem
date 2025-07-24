using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Entities
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string RoleName { get; set; }
    }
}
