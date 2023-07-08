using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Requests.Account;
using PMS.Infrastructure.Services.Interfaces;

namespace PMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;



        public AccountController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]Register request)
        {
            if(request == null)
                return BadRequest();

            await _userService.AddAsync(request.Email, request.Password, request.FirstName, request.LastName, request.PhoneNumber);

            return Created("/Account", null);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]Login request)
        {
            if (request == null)
                return BadRequest();

            var token = await _userService.LoginAsync(request.Email, request.Password);

            if (token == null)
                return Unauthorized();

            return new JsonResult(token);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var user = await _userService.GetAsync(Guid.Parse(User.Identity!.Name!));

            if(user == null)
                return NotFound();

            return new JsonResult(user);
        }
    }
}