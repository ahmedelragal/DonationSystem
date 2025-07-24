using DonationSystem.API.Controllers;
using DonationSystem.Application.DTOs;
using DonationSystem.Application.Features.Auth.Commands.ChangePassword;
using DonationSystem.Application.Features.Auth.Commands.Login;
using DonationSystem.Application.Features.Auth.Commands.Register;
using DonationSystem.Application.Features.Auth.Commands.Role;
using DonationSystem.Application.Features.Auth.Commands.UpdateUser;
using DonationSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DonationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var result = await _mediator.Send(new RegisterUserCommand(dto));
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
        {
            var tokens = await _mediator.Send(cmd);
            return Ok(tokens);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _mediator.Send(new ChangePasswordCommand(userId, dto));
            return Ok(new { message = result });
        }
        
        [HttpPost("add-role/{id}")]
        public async Task<IActionResult> AddRole(Guid id, [FromBody] ModifyUserRoleDto dto)
        {
            var result = await _mediator.Send(new AddUserRoleCommand(id, dto.RoleName));
            return Ok(new { message = result });
        }
        [HttpPost("remove-role/{id}")]
        public async Task<IActionResult> RemoveRole(Guid id, [FromBody] ModifyUserRoleDto dto)
        {
            var result = await _mediator.Send(new RemoveUserRoleCommand(id, dto.RoleName));
            return Ok(new { message = result });
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _mediator.Send(new UpdateUserCommand(userId, dto));

            if (!result)
                return NotFound(new ApiResponse<string> { Message = "User not found", Data = null });

            return Ok(new ApiResponse<string> { Message = "Profile updated successfully" });
        }
    }
}
