using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyInventory.Database;
using CompanyInventory.Database.Entities;
using CompanyInventory.Models.Company;
using CompanyInventory.Repository.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CompanyInventory.Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(CompanyInventoryContext context)
            : base(context)
        {
        }

        public async Task<NewCompanyResponse> AddAsync(NewCompanyRequest model)
        {
            var result = await AddAsync(new Company
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

            await SaveChangesAsync();

            return new NewCompanyResponse
            {
                Id = result.Entity.Id
            };
        }

        public async Task<List<CompanyResult>> GetCompaniesEmployeesAsync(CompanySearch model)
        {
            var predicate = GetCompaniesEmployeesFilter(model);

            return await Context.Companies
                .AsNoTracking()
                .Include(i => i.Employees)
                .Where(predicate)
                .Select(s => new CompanyResult
                {
                    Name = s.Name,
                    FoundationYear = s.FoundationYear,
                    Employees = s.Employees.Select(x => new CompanyEmployeeResult
                    {
                        BirthDate = x.BirthDate,
                        FirstName = x.FirstName,
                        JobTitle = x.JobTitle,
                        LastName = x.LastName
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task UpdateAsync(long id, NewCompanyRequest model)
        {
            var company = await GetByIdAsync(id);

            if (company == null)
            {
                throw new Exception("Company does not exist.");
            }

            UpdateCompanyModel(company, model);

            await UpdateAsync(company);

            await SaveChangesAsync();
        }

        private ExpressionStarter<Company> GetCompaniesEmployeesFilter(CompanySearch model)
        {
            var predicate = PredicateBuilder.New<Company>(true);

            if (model.EmployeeJobTitles != null && model.EmployeeJobTitles.Count > 0)
            {
                predicate.And(a =>
                    a.Employees.Any(x => model.EmployeeJobTitles.Contains(x.JobTitle)));
            }

            if (model.EmployeeBirthDateFrom.HasValue)
            {
                predicate.And(a =>
                    a.Employees.Any(x => x.BirthDate >= model.EmployeeBirthDateFrom.Value));
            }

            if (model.EmployeeBirthDateTo.HasValue)
            {
                predicate.And(a =>
                    a.Employees.Any(x => x.BirthDate <= model.EmployeeBirthDateTo.Value));
            }

            if (!string.IsNullOrWhiteSpace(model.Keyword))
            {
                predicate.And(a => a.Name.ToLower().Contains(model.Keyword.ToLower())
                                   || a.Employees.Any(x =>
                                       x.FirstName.ToLower().Contains(model.Keyword.ToLower())
                                       || x.LastName.ToLower().Contains(model.Keyword.ToLower())));
            }

            return predicate;
        }

        private void UpdateCompanyModel(Company company, NewCompanyRequest model)
        {
            company.Name = model.Name;
            company.FoundationYear = model.FoundationYear;
            company.Employees = model.Employees
                .Select(s => new Employee
                {
                    FirstName = s.FirstName,
                    BirthDate = s.BirthDate,
                    JobTitle = s.JobTitle,
                    LastName = s.LastName
                }).ToList();
        }

        public async Task DeleteAsync(long id)
        {
            var company = await GetByIdAsync(id);

            if (company == null)
            {
                throw new Exception("Company does not exist.");
            }

            await RemoveAsync(company);

            await SaveChangesAsync();
        }

        private async Task<Company> GetByIdAsync(long id)
            => await Context.Companies
                .Include(i => i.Employees)
                .FirstOrDefaultAsync(f => f.Id == id);
    }
}