using Autofac;
using CompanyInventory.Repository.Interfaces;

namespace CompanyInventory.Repository
{
    public static class IoC
    {
        public static void RegisterRepositories(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ICompanyRepository).Assembly)
                .Where(w => w.Name.Contains("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
