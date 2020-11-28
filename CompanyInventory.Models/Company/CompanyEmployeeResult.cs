using System;
using CompanyInventory.Common.Enums;

namespace CompanyInventory.Models.Company
{
    public class CompanyEmployeeResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}