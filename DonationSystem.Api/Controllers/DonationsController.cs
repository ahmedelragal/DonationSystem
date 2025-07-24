using DonationSystem.Application.DTOs;
using DonationSystem.Application.Features.Donations.Commands.CreateDonation;
using DonationSystem.Application.Features.Donations.Commands.DeleteDonation;
using DonationSystem.Application.Features.Donations.Queries.GetDonations;
using DonationSystem.Application.Features.Donations.Queries.GetUserDonations;
using DonationSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateDonationCommand command)
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
                Message = "Donations Obtained Successfully",
                Data = donations
            });
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var donation = await _mediator.Send(new GetDonationByIdQuery(id));
            return Ok(new ApiResponse<object>
            {
                Message = "Donation Found Successfully",
                Data = donation
            });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteDonationCommand(id));
            return Ok(new ApiResponse<object>
            {
                Message = "Donation Deleted Successfully",
                Data = null
            });
        }
        
    }
}
