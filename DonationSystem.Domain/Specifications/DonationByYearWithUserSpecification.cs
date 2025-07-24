using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class DonationByYearWithUserSpecification : Specification<Donation>
    {
        public DonationByYearWithUserSpecification(int year) : base(d => d.DonatedAt.Year == year)
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Images);
        }
    }
}
