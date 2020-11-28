using CompanyInventory.Models.Company;
using CompanyInventory.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInventory.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NewCompanyRequest model)
        {
            return Ok("Id");
        }
        
        [HttpPost("search")]
        public IActionResult Search([FromBody] object request)
        {
            return Ok("result");
        }
        
        [HttpPut("update/{id}")]
        public IActionResult Update(long id,[FromBody] object request)
        {
            return Ok("result");
        }
        
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(long id)
        {
            return Ok("result");
        }
    }
}