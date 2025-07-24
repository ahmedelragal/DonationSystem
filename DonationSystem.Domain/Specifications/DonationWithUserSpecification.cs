using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class DonationWithUserSpecification : Specification<Donation>
    {
        public DonationWithUserSpecification()
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Images);
            AddOrderBy(d => d.Amount);
        }
        public DonationWithUserSpecification(Guid UserId) : base(d => d.UserId == UserId)
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Images);
            AddOrderBy(d => d.Amount);
        }
    }
}
