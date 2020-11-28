using System.Collections.Generic;

namespace CompanyInventory.Models.Company
{
    public class CompanyResult
    {
        public string Name { get; set; }
        public short FoundationYear { get; set; }
        public List<CompanyEmployeeResult> Employees { get; set; }
    }
}