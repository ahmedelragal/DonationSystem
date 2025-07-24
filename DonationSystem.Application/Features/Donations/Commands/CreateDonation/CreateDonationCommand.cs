using DonationSystem.Application.DTOs.Donations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Commands.CreateDonation
{
    public record CreateDonationCommand(CreateDonationDto Donation) : IRequest<Guid>;
}
