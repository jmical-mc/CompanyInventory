using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CompanyInventory.Common.Enums;
using CompanyInventory.Models.Company;
using CompanyInventory.Models.Employee;
using CompanyInventory.Repository.Test;
using NUnit.Framework;

namespace CompanyInventory.Repository.Tests.Repositories
{
    public class CompanyRepositoryTests : BaseRepositories
    {
        private readonly NewCompanyRequest _newCompany = new NewCompanyRequest
        {
            Name = "Company",
            FoundationYear = 2020,
            Employees = new List<EmployeeCompany>
            {
                new EmployeeCompany
                {
                    FirstName = "Jakub",
                    LastName = "Micał",
                    JobTitle = JobTitle.Developer,
                    BirthDate = new DateTime(1999, 01, 01)
                }
            }
        };

        [Test]
        public async Task Add_Valid_Company_With_Employee()
        {
            //Arrange

            //Act
            var companyResult = await CompanyRepository.AddAsync(_newCompany);

            await CompanyRepository.DeleteAsync(companyResult.Id);

            //Assert
            Assert.IsTrue(companyResult.Id > 0);
        }

        [Test]
        public async Task Get_Companies_Employees_Without_Search()
        {
            //Arrange
            var searchModel = new CompanySearch();

            //Act
            var companyResult = await CompanyRepository.AddAsync(_newCompany);

            var companies = await CompanyRepository.GetCompaniesEmployeesAsync(searchModel);

            await CompanyRepository.DeleteAsync(companyResult.Id);

            //Assert
            Assert.IsTrue(companies.Count > 0);
        }
        
        [Test]
        public async Task Get_Companies_Employees_With_Search()
        {
            //Arrange
            var searchModel = new CompanySearch
            {
                Keyword = "jakub",
                EmployeeJobTitles = new List<JobTitle> {JobTitle.Developer},
                EmployeeBirthDateFrom = new DateTime(1998,01,01),
                EmployeeBirthDateTo = new DateTime(2000,01,01),
            };

            //Act
            var companyResult = await CompanyRepository.AddAsync(_newCompany);

            var companies = await CompanyRepository.GetCompaniesEmployeesAsync(searchModel);

            await CompanyRepository.DeleteAsync(companyResult.Id);

            //Assert
            Assert.IsTrue(companies.Count > 0);
        }
        
        [Test]
        public async Task Update_Valid_Company_With_Employee()
        {
            //Arrange
            var isThrown = false;
            var companyResult = new NewCompanyResponse();

            //Act
            try
            {
                companyResult = await CompanyRepository.AddAsync(_newCompany);

                await CompanyRepository.UpdateAsync(companyResult.Id, _newCompany);
            }
            catch (Exception)
            {
                isThrown = true;
            }

            await CompanyRepository.DeleteAsync(companyResult.Id);

            //Assert
            Assert.IsFalse(isThrown);
        }

        [Test]
        public async Task Delete_Existing_Company_With_Employee()
        {
            //Arrange
            var isThrown = false;
            
            //Act
            var companyResult = await CompanyRepository.AddAsync(_newCompany);

            try
            {
                await CompanyRepository.DeleteAsync(companyResult.Id);
            }
            catch (Exception)
            {
                isThrown = true;
            }
     
            //Assert
            Assert.IsFalse(isThrown);
        }
    }
}