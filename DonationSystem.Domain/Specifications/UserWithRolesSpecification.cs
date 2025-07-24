using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class UserWithRolesSpecification : Specification<User>
    {
        public UserWithRolesSpecification(Guid Id) : base(u => u.Id == Id)
        {
            AddInclude(u => u.UserRoles);
        }
    }
}
