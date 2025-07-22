using DonationSystem.Application.DTOs;
using DonationSystem.Application.Features.Donations.Commands.CreateDonation;
using DonationSystem.Application.Features.Donations.Queries.GetDonations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DonationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateDonationCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new ApiResponse<object>
            {
                Message = "Donation Submitted Successfully",
                Data = new { DonationId = id }
            });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _mediator.Send(new GetDonationsQuery());
            return Ok(new ApiResponse<object>
            {
                Message = "Donation Obtained Successfully",
                Data = donations
            });
        }
    }
}
