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
using System;

namespace Twajd_Back_End.Root
{
    public class CompositionRoot
    {
        public CompositionRoot() { }

         

        public static void injectDependencies(IServiceCollection services, IConfiguration Configuration)
        {
            DotNetEnv.Env.Load("../.env");

            services.AddDbContext<DatabaseContext>(p =>
               p.UseNpgsql(Configuration.GetConnectionString("Default")
               //p.UseNpgsql(System.Environment.GetEnvironmentVariable("ConnectionString")
               ));


            services.AddIdentity<ApplicationUser, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddTransient<IManagerService, ManagerService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IWorkHoursService, WorkHoursService>();
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<IAuthService, AuthService>();

        }
    }
}
