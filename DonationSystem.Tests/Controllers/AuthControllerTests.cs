using DonationSystem.Api.Controllers;
using DonationSystem.Application.DTOs;
using DonationSystem.Application.DTOs.Auth;
using DonationSystem.Application.Features.Auth.Commands.ChangePassword;
using DonationSystem.Application.Features.Auth.Commands.Login;
using DonationSystem.Application.Features.Auth.Commands.Register;
using DonationSystem.Application.Features.Auth.Commands.Role;
using DonationSystem.Application.Features.Auth.Commands.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace DonationSystem.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new AuthController(_mediatorMock.Object);
        }

        // ==================== Register ====================

        [Fact]
        public async Task Register_ShouldReturnOk_WithMessage()
        {
            var dto = new RegisterUserDto { Email = "test@gmail.com", FullName = "Test", Password = "123456" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default))
                .ReturnsAsync("User registered successfully");

            var result = await _controller.Register(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);
            Assert.Equal("User registered successfully", response.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnBadRequest_WhenMediatorReturnsNull()
        {
            var dto = new RegisterUserDto { Email = "test@gmail.com", FullName = "Test", Password = "123456" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), default))
                .ReturnsAsync((string?)null);

            var result = await _controller.Register(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Registration failed", badRequest.Value);
        }

        // ==================== Login ====================

        [Fact]
        public async Task Login_ShouldReturnTokens()
        {
            var command = new LoginCommand("test@gmail.com", "123456");
            var tokens = new LoginResponseDto { Token = "abc", RefreshToken = "xyz" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
                .ReturnsAsync(tokens);

            var result = await _controller.Login(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsType<ApiResponse<LoginResponseDto>>(okResult.Value);
            Assert.Equal("abc", apiResponse.Data.Token);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            var command = new LoginCommand("wrong@gmail.com", "wrong");

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<LoginCommand>(), default))
                .ReturnsAsync((LoginResponseDto?)null);

            var result = await _controller.Login(command);

            Assert.IsType<UnauthorizedResult>(result);
        }

        // ==================== ChangePassword ====================

        [Fact]
        public async Task ChangePassword_ShouldReturnOk_WhenUserIdIsValid()
        {
            var dto = new ChangePasswordDto { CurrentPassword = "old", NewPassword = "new", ConfirmPassword = "new" };
            var userId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default))
                .ReturnsAsync("Password changed successfully.");

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(claims)) }
            };

            var result = await _controller.ChangePassword(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);
            Assert.Equal("Password changed successfully.", response.Message);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnUnauthorized_WhenNoClaim()
        {
            var dto = new ChangePasswordDto { CurrentPassword = "old", NewPassword = "new", ConfirmPassword = "new" };

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var result = await _controller.ChangePassword(dto);

            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnBadRequest_WhenMediatorReturnsNull()
        {
            var dto = new ChangePasswordDto { CurrentPassword = "old", NewPassword = "new", ConfirmPassword = "new" };
            var userId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), default))
                .ReturnsAsync((string?)null);

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(claims)) }
            };

            var result = await _controller.ChangePassword(dto);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Password change failed", badRequest.Value);
        }

        // ==================== Refresh Token ====================
        [Fact]
        public async Task RefreshToken_ShouldReturnOk_WithLoginResponseDto()
        {
            var command = new RefreshTokenCommand("dummy-token");
            var responseDto = new LoginResponseDto { Token = "jwt-token", RefreshToken = "refresh-token" };

            _mediatorMock
                .Setup(m => m.Send(command, default))
                .ReturnsAsync(responseDto);

            var result = await _controller.RefreshToken(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<LoginResponseDto>>(okResult.Value);

            Assert.Equal("Success", response.Message);
            Assert.Equal(responseDto, response.Data);
        }
        // ==================== Add Role ====================
        [Fact]
        public async Task AddRole_ShouldReturnOk_WithSuccessMessage()
        {
            var userId = Guid.NewGuid();
            var dto = new ModifyUserRoleDto { RoleName = "Admin" };
            var expectedMessage = "Role added successfully";

            _mediatorMock
                .Setup(m => m.Send(It.Is<AddUserRoleCommand>(cmd => cmd.UserId == userId && cmd.RoleName == dto.RoleName), default))
                .ReturnsAsync(expectedMessage);

            var result = await _controller.AddRole(userId, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);

            Assert.Equal(expectedMessage, response.Message);
        }
        // ==================== Remove Role ====================
        [Fact]
        public async Task RemoveRole_ShouldReturnOk_WithSuccessMessage()
        {
            var userId = Guid.NewGuid();
            var dto = new ModifyUserRoleDto { RoleName = "User" };
            var expectedMessage = "Role removed successfully";

            _mediatorMock
                .Setup(m => m.Send(It.Is<RemoveUserRoleCommand>(cmd => cmd.UserId == userId && cmd.RoleName == dto.RoleName), default))
                .ReturnsAsync(expectedMessage);

            var result = await _controller.RemoveRole(userId, dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);

            Assert.Equal(expectedMessage, response.Message);
        }
        // ==================== UpdateProfile  ====================

        [Fact]
        public async Task UpdateProfile_ShouldReturnOk_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var dto = new UpdateUserDto { FullName = "Updated Name", Email = "updated@example.com" };

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(identity)
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.Is<UpdateUserCommand>(c => c.UserId == userId && c.Dto == dto), default))
                .ReturnsAsync(true);

            var result = await _controller.UpdateProfile(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<ApiResponse<object>>(okResult.Value);

            Assert.Equal("Profile updated successfully", response.Message);
        }

        [Fact]
        public async Task UpdateProfile_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var dto = new UpdateUserDto { FullName = "Updated", Email = "nope@example.com" };

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(identity)
                }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), default))
                .ReturnsAsync(false);

            var result = await _controller.UpdateProfile(dto);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var response = Assert.IsType<ApiResponse<string>>(notFoundResult.Value);

            Assert.Equal("User not found", response.Message);
        }


    }
}
