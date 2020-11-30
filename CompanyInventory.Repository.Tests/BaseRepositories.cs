using System;
using Autofac;
using CompanyInventory.Database;
using CompanyInventory.Repository.Interfaces;
using CompanyInventory.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyInventory.Repository.Test
{
    public class BaseRepositories
    {
        public ICompanyRepository CompanyRepository { get; set; }

        public BaseRepositories()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
            builder.RegisterType<CompanyInventoryContext>().As<CompanyInventoryContext>();
            builder.RegisterType<DbContextOptions<CompanyInventoryContext>>()
                .As<DbContextOptions<CompanyInventoryContext>>();

            var container = builder.Build();

            CompanyRepository = container.Resolve<ICompanyRepository>();
        }
    }
}