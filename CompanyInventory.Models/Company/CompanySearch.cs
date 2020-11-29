using System;
using System.Collections.Generic;
using CompanyInventory.Common.Enums;
using FluentValidation;

namespace CompanyInventory.Models.Company
{
    public class CompanySearch
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeBirthDateFrom { get; set; }
        public DateTime? EmployeeBirthDateTo { get; set; }
        public List<JobTitle> EmployeeJobTitles { get; set; }
    }
    
    public class CompanySearchValidator : AbstractValidator<CompanySearch>
    {
        public CompanySearchValidator()
        {
            RuleFor(x => x.Keyword)
                .MaximumLength(500)
                .WithMessage("Keyword field should have max 500 characters");
            
            RuleFor(x => x.EmployeeJobTitles)
                .IsInEnum()
                .WithMessage("Given job title does not exist");
        }
    }
}