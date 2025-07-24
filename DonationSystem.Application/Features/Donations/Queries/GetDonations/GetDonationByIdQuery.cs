using DonationSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Queries.GetDonations
{
    public record GetDonationByIdQuery (Guid Id) : IRequest<DonationDto>;
}
