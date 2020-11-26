using Microsoft.AspNetCore.Mvc;

namespace CompanyInventory.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        public CompanyController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("works");
        }
    }
}