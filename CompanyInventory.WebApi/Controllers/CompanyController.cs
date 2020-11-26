using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyInventory.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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