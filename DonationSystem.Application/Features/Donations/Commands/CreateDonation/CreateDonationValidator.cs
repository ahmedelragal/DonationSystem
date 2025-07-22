using DonationSystem.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonationSystem.Application.Features.Donations.Commands.CreateDonation
{
    public class CreateDonationValidator : AbstractValidator<CreateDonationDto>
    {
        public CreateDonationValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
