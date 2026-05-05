using APBD_TEST_TEMPLATE.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APBD_TEST_TEMPLATE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetVendors([FromQuery] string? name)
        {
            var vendors = await _vendorService.GetVendors(name);
            if (vendors.Count == 0)
            {
                return BadRequest();
            }
            return Ok(vendors);
        }
    }
}
