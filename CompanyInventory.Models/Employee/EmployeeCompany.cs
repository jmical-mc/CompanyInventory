using System;
using CompanyInventory.Common.Enums;

namespace CompanyInventory.Models.Employee
{
    public class EmployeeCompany
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}