using System.Collections.Generic;
using CompanyInventory.Models.Employee;
using FluentValidation;

namespace CompanyInventory.Models.Company
{
    public class NewCompanyRequest
    {
        public string Name { get; set; }
        public short FoundationYear { get; set; }
        public List<EmployeeCompany> Employees { get; set; }
    }
    
    public class NewCompanyRequestValidator : AbstractValidator<NewCompanyRequest>
    {
        public NewCompanyRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Ensure that you have entered company name");

            RuleFor(x => x.Name)
                .MaximumLength(500)
                .WithMessage("Company name can max 500 characters");
            
            RuleFor(x => x.FoundationYear)
                .GreaterThanOrEqualTo((short) 1000)
                .WithMessage("Foundation year must be greater than 1000 year");
            
            RuleFor(x => x.FoundationYear)
                .LessThanOrEqualTo((short)9999)
                .WithMessage("Foundation year must be less than 9999 year");
        }
    }
}