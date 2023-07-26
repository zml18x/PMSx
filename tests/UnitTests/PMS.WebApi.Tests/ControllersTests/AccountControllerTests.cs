using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PMS.Core.Entities;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Requests.Account;
using PMS.Infrastructure.Services.Interfaces;
using PMS.WebApi.Controllers;
using System.Security.Claims;

namespace PMS.WebApi.Tests.ControllersTests
{
    public class AccountControllerTests
    {
        private Mock<IUserService> _userService;
        private AccountController _accountController;

        
        public AccountControllerTests()
        {
            _userService = new Mock<IUserService>();
            _accountController = new AccountController(_userService.Object);
        }


        [Fact]
        public async Task RegisterAsync_ShouldInvokeRegisterAsyncOnUserServiceAndRetrun201StatusCode()
        {
            var email = "test@mail.com";
            var password = "pa$SW0rD!";
            var firstName = "Test";
            var lastName = "TestTest";
            var phoneNumber = "123456789";

            var request = new Register(email, password, firstName, lastName, phoneNumber);


            var result = await _accountController.RegisterAsync(request);
            var createdResult = (CreatedResult)result;

            _userService.Verify(x => x.RegisterAsync(email, password, firstName, lastName, phoneNumber));
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, createdResult!.StatusCode);
            Assert.Equal("/Account", createdResult.Location);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsBadRequestWhenRequestIsNull()
        {
            var result = await _accountController.RegisterAsync(null!);
            var badRequestResult = (BadRequestResult)result;

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult!.StatusCode);
        }

        [Fact]
        public async Task LoginAsync_ReturnsBadRequestWhenRequestIsNull()
        {
            var result = await _accountController.LoginAsync(null!);
            var badRequestResult = (BadRequestResult)result;

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult!.StatusCode);
        }

        [Fact]
        public async Task LoginAsync_ReturnsUnauthorizedWhenTokenIsNull()
        {
            var email = "test@mail.com";
            var password = "testPassword!#23";

             _userService.Setup(x => x.LoginAsync(email, password)).ReturnsAsync((JwtDto)null!);

            var request = new Login(email, password);

            var result = await _accountController.LoginAsync(request);
            var unauthorizedResult = (UnauthorizedResult)result;

            Assert.NotNull(result);
            Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, unauthorizedResult!.StatusCode);
        }

        [Fact]
        public async Task LoginAsync_ShouldInvokeLoginAsyncOnUserServiceAndReturnJwtDtoAsJson()
        {
            var email = "test@mail.com";
            var password = "testPassword!#23";

            var token = "testToken";
            var expires = DateTime.UtcNow.AddMinutes(30);
            var role = "User";

            var jwtDto = new JwtDto(token, expires, role);

            _userService.Setup(x => x.LoginAsync(email, password)).ReturnsAsync(jwtDto);

            var request = new Login(email, password);

            var result = await _accountController.LoginAsync(request);
            var jsonResult = (JsonResult)result;

            dynamic data = jsonResult!.Value!;

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(token, data.Token);
            Assert.Equal(expires, data.Expires);
            Assert.Equal(role, data.Role);
        }

        [Fact]
        public async Task GetAsync_ReturnsNotFoundWhenUserServiceReturnsNull()
        {
            var userId = Guid.NewGuid();

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            _userService.Setup(x => x.GetAsync(userId))!.ReturnsAsync((UserDto)null!);

            var result = await _accountController.GetAsync();
            var notFoundResult = (NotFoundResult)result;

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404,notFoundResult!.StatusCode);
        }

        [Fact]
        public async Task GetAsync_ShouldInvokeGetAsyncOnUserServiceAndReturnAccountDtoInJson()
        {
            var userId = Guid.NewGuid();

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            var userDto = new UserDto(userId, Guid.NewGuid(), "User", "test@mail.com");
            _userService.Setup(x => x.GetAsync(userId)).ReturnsAsync(userDto);

            var result = await _accountController.GetAsync();
            var jsonResult = (JsonResult)result;

            _userService.Verify(x => x.GetAsync(userId), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(userDto, jsonResult.Value);
        }

        [Fact]
        public async Task GetProfileAsync_ReturnsNotFoundWhenUserServiceReturnsNull()
        {
            var userId = Guid.NewGuid();

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            _userService.Setup(x => x.GetDetailsAsync(userId))!.ReturnsAsync((UserProfileDto)null!);

            var result = await _accountController.GetProfileAsync();
            var notFoundResult = (NotFoundResult)result;

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult!.StatusCode);
        }

        [Fact]
        public async Task GetProfileAsync_ShouldInvokeGetAsyncOnUserServiceAndReturnAccountDtoInJson()
        {
            var userId = Guid.NewGuid();

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            var userProfileDto = new UserProfileDto(userId, Guid.NewGuid(), "User", "test@mail.com","FirstName","LastName","123456789");
            _userService.Setup(x => x.GetDetailsAsync(userId)).ReturnsAsync(userProfileDto);

            var result = await _accountController.GetProfileAsync();
            var jsonResult = (JsonResult)result;

            _userService.Verify(x => x.GetDetailsAsync(userId), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(userProfileDto, jsonResult.Value);
        }

        [Fact]
        public async Task UpdateProfileAsync_ReturnsBadRequestWhenRequestIsNull()
        {
            var result = await _accountController.UpdateProfileAsync(null!);
            var badRequestResult = (BadRequestResult)result;

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult!.StatusCode);
        }

        [Fact]
        public async Task UpdateProfileAsync_ShouldInvokeUpdateAsyncOnUserServiceAndReturn200StatusCode()
        {
            var userId = Guid.NewGuid();
            var firstName = "TestName";
            var lastName = "TestLastName";
            var phoneNumber = "TestPhoneNumber";

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }, "test"))
                }
            };

            var request = new UpdateProfile(firstName, lastName, phoneNumber);

            var result = await _accountController.UpdateProfileAsync(request);
            var okResult = (OkResult)result;

            _userService.Verify(x => x.UpdateProfileAsync(userId, firstName, lastName, phoneNumber));
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult!.StatusCode);
        }
    }
}