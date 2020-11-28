using System;
using System.Collections.Generic;
using CompanyInventory.Common.Enums;

namespace CompanyInventory.Models.Company
{
    public class CompanySearch
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeBirthDateFrom { get; set; }
        public DateTime? EmployeeBirthDateTo { get; set; }
        public List<JobTitle> EmployeeJobTitles { get; set; }
    }
}