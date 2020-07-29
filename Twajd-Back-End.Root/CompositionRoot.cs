using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Twajd_Back_End.Business.Services;
using Twajd_Back_End.Core.Models.Auth;
using Twajd_Back_End.Core.Repositories;
using Twajd_Back_End.Core.Services;
using Twajd_Back_End.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Twajd_Back_End.Core.Settings;

namespace Twajd_Back_End.Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

         

        public static void injectDependencies(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DatabaseContext>(p =>
               p.UseNpgsql(Configuration.GetConnectionString("Default")));
            services.AddScoped<DatabaseContext>();

            services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAuthService, AuthService>();
            


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
