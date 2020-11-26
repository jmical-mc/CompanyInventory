using System.Collections.Generic;

namespace CompanyInventory.Database.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public short FoundationYear { get; set; }
        
        public virtual List<Employee> Employees { get; set; }
    }
}
