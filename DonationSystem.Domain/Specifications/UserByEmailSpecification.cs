using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class UserByEmailSpecification : Specification<User>
    {
        public UserByEmailSpecification(string email) : base(u => u.Email == email)
        {
        }
    }
}
