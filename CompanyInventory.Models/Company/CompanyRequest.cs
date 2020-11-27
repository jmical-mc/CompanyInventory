using System.Collections.Generic;
using CompanyInventory.Models.Employee;

namespace CompanyInventory.Models.Company
{
    public class CompanyRequest
    {
        public string Name { get; set; }
        public short FoundationYear { get; set; }
        public List<EmployeeCompany> Employees { get; set; }
    }
}