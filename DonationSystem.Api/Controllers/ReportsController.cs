using DonationSystem.Application.DTOs;
using DonationSystem.Application.Features.Donations.Queries.GetUserDonations;
using DonationSystem.Application.Features.Reports.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("user-donations/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserDonations(Guid userId)
        {
            var donations = await _mediator.Send(new GetMyDonationsQuery(userId));
            return Ok(new ApiResponse<object>
            {
                Message = "User donations retrieved successfully",
                Data = donations
            });
        }

        [HttpGet("monthly-report/{year}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetMonthlyReport(int year)
        {
            var result = await _mediator.Send(new GetMonthlyDonationReportQuery(year));
            return Ok(new ApiResponse<object>
            {
                Message = "Monthly Report Generated Successfully",
                Data = result
            });
        }

        [HttpGet("report/by-date")]
        public async Task<IActionResult> GetDonationsByDateRange([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var result = await _mediator.Send(new DateFilteredDonationReportQuery(from, to));
            return Ok(result);
        }
    }
}
