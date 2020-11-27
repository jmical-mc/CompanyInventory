using System.Linq;
using System.Threading.Tasks;
using CompanyInventory.Database;
using CompanyInventory.Database.Entities;
using CompanyInventory.Models.Company;
using CompanyInventory.Repository.Interfaces;

namespace CompanyInventory.Repository.Repositories
{
    public class CompanyRepository : BaseRepository, ICompanyRepository
    {
        public CompanyRepository(CompanyInventoryContext context)
            : base(context)
        {
        }

        public async Task<CompanyResponse> Add(CompanyRequest model)
        {
            var result = await Context.Companies.AddAsync(new Company
            {
                Name = model.Name,
                FoundationYear = model.FoundationYear,
                Employees = model.Employees
                    .Select(s => new Employee
                    {
                        JobTitle = s.JobTitle,
                        BirthDate = s.BirthDate,
                        FirstName = s.FirstName,
                        LastName = s.LastName
                    }).ToList()
            });

            return new CompanyResponse
            {
                Id = result.Entity.Id
            };
        }
        
        public async Task<CompanyResponse> GetCompaniesEmployees(CompanyRequest model)
        {
            var result = await Context.Companies.AddAsync(new Company
            {
                Name = model.Name,
                FoundationYear = model.FoundationYear,
                Employees = model.Employees
                    .Select(s => new Employee
                    {
                        JobTitle = s.JobTitle,
                        BirthDate = s.BirthDate,
                        FirstName = s.FirstName,
                        LastName = s.LastName
                    }).ToList()
            });

            return new CompanyResponse
            {
                Id = result.Entity.Id
            };
        }
    }
}