using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Infrastructure.Requests.Property;
using PMS.Infrastructure.Services.Interfaces;

namespace PMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IAddressService _addressService;



        public PropertyController(IPropertyService propertyService, IAddressService addressService)
        {
            _propertyService = propertyService;
            _addressService = addressService;
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> AddNewProperty([FromBody] CreateProperty request)
        {
            if (request == null)
                return BadRequest();

            try
            {
                var userId = Guid.Parse(User.Identity!.Name!);

                if (userId == Guid.Empty)
                    throw new ArgumentNullException(nameof(userId), "UserId cannot be null");

                var addressId = Guid.NewGuid();

                await _addressService.CreateAsync(addressId, request.CountryCode, request.Region, request.City,
                    request.PostalCode, request.Street, request.BuildingNumber);

                await _propertyService.CreateAsync(userId, addressId, request.PropertyType,
                    request.Stars, request.PropertyName, request.Description, request.MaxRoomsCount);

                return Created("Propert/test", null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}