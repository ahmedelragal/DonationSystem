using DonationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DonationSystem.Domain.Specifications
{
    public class DonationWithUserImageSpecifiaction : Specification<Donation>
    {
        public DonationWithUserImageSpecifiaction(Guid donationId) : base(d => d.Id == donationId) 
        {
            AddInclude(d => d.Images); 
            AddInclude(d => d.User);
        }
    }
}
