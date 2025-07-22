using DonationSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Queries.GetDonations
{
    public class GetDonationsQuery : IRequest<List<DonationDto>>
    {
    }
}
