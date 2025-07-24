using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Domain.Specifications
{
    public class DonationByDateRangeSpecification : Specification<Donation>
    {
        public DonationByDateRangeSpecification(DateTime? from, DateTime? to)
            : base(d =>
                (!from.HasValue || d.DonatedAt >= from.Value) &&
                (!to.HasValue || d.DonatedAt <= to.Value))
        {
            AddInclude(d => d.User);
            AddInclude(d => d.Images);
        }
    }
}
