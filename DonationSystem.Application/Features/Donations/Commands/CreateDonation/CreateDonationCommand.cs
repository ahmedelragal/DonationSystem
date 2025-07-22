using DonationSystem.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Commands.CreateDonation
{
    public class CreateDonationCommand : IRequest<Guid>
    {
        public CreateDonationDto Donation { get; set; } = null!;
    }
}
