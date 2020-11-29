using System.Net;
using System.Threading.Tasks;
using CompanyInventory.Models.Company;
using CompanyInventory.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInventory.WebApi.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> CreateAsync([FromBody] NewCompanyRequest model)
        {
            var company = await _companyRepository.AddAsync(model);

            return StatusCode((int) HttpStatusCode.Created, company);
        }

        [AllowAnonymous]
        [HttpPost("search")]
        public async Task<IActionResult> GetCompaniesAsync([FromBody] CompanySearch request)
        {
            var companies = await _companyRepository.GetCompaniesEmployeesAsync(request);

            return Ok(companies);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] NewCompanyRequest request)
        {
            await _companyRepository.UpdateAsync(id, request);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _companyRepository.DeleteAsync(id);

            return Ok();
        }
    }
}