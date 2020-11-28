using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyInventory.Models.Company;

namespace CompanyInventory.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<NewCompanyResponse> AddAsync(NewCompanyRequest model);
        Task<List<CompanyResult>> GetCompaniesEmployeesAsync(CompanySearch model);
        Task UpdateAsync(long id, NewCompanyRequest model);
        Task DeleteAsync(long id);
    }
}
