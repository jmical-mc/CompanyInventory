using System;
using CompanyInventory.Common.Enums;
using FluentValidation;

namespace CompanyInventory.Models.Employee
{
    public class EmployeeCompany
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public JobTitle JobTitle { get; set; }
    }

    public class EmployeeCompanyValidator : AbstractValidator<EmployeeCompany>
    {
        public EmployeeCompanyValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ensure that you have entered first name");
            
            RuleFor(x => x.FirstName)
                .MaximumLength(500)
                .WithMessage("First name can max 500 characters");
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Ensure that you have entered last name");
                
            RuleFor(x => x.LastName)
                .MaximumLength(500)
                .WithMessage("Last name can max 500 characters");
            
            RuleFor(x => x.BirthDate)
                .NotNull()
                .WithMessage("Ensure that you have entered date of birth");
            
            RuleFor(x => x.BirthDate)
                .Must(m => m <= DateTime.Now)
                .WithMessage("Date of birth cannot be future");
            
            RuleFor(x => x.JobTitle)
                .NotNull()
                .WithMessage("Ensure that you have entered job title");
            
            RuleFor(x => x.JobTitle)
                .IsInEnum()
                .WithMessage("Given job title does not exist");
        }
    }
}