using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Twajd_Back_End.Business.Services.Impl;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;
using Twajd_Back_End.DataAccess.Repositories;

namespace Twajd_Back_End.Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

        public static void injectDependencies(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(p =>
               p.UseNpgsql("Server=127.0.0.1;port=5432;user id = postgres ;password = 7533; database=TwjdTest; pooling = true"));
            services.AddScoped<DatabaseContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<DatabaseContext>(opts => opts.UseInMemoryDatabase("database"));
            //services.AddScoped<DatabaseContext>();
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<IAuthorRepository, AuthorRepository>();
            //services.AddScoped<IAuthorService, AuthorService>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
