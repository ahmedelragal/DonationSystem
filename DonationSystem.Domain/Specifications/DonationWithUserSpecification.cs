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
        public DonationWithUserSpecification() : base(g=>g.Amount >= 10)
        {
            AddInclude(d => d.User);
            AddOrderBy(d => d.Amount);
        }
    }
}
