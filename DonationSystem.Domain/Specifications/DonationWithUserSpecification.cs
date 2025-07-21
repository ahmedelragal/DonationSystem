using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class DonationWithUserSpecification : BaseSpecification<Donation>
    {
        public DonationWithUserSpecification()
        {
            AddInclude(d => d.User);
        }

        public DonationWithUserSpecification(Guid id)
            : base(d => d.Id == id)
        {
            AddInclude(d => d.User);
        }
    }
}
