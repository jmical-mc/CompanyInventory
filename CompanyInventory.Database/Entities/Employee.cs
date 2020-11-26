using System;
using CompanyInventory.Common.Enums;

namespace CompanyInventory.Database.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Position Position { get; set; }

        public virtual Company Company { get; set; }
        public long CompanyId { get; set; }
    }
}
