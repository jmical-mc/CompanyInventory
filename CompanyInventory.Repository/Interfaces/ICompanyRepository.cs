using System.Threading.Tasks;
using CompanyInventory.Models.Company;

namespace CompanyInventory.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<CompanyResponse> Add(CompanyRequest model);
    }
}
