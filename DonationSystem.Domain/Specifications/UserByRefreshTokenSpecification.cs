using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class UserByRefreshTokenSpecification : Specification<User>
    {
        public UserByRefreshTokenSpecification(string refreshToken) : base(u => u.RefreshToken == refreshToken)
        {
            AddInclude(u=>u.UserRoles);
        }
    }
}
