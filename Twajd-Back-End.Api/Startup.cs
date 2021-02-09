using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Twajd_Back_End.Api.Extensions;
using Twajd_Back_End.Api.Middleware;
using Twajd_Back_End.Core.Settings;
using Twajd_Back_End.Root;

namespace Twajd_Back_End.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = Configuration["redis:connectionString"];
            });

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();


            services.AddControllers();

            CompositionRoot.injectDependencies(services, Configuration);       

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Docs", Version = "V1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                UnresolvedReference = true
                            },
                            new List<string>()
                        }
                    };
                options.AddSecurityRequirement(security);
                //c.EnableAnnotations();
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, "Twajd-Back-End.xml");
                // options.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddAuth(jwtSettings);
            services.AddTransient<TokenManagerMiddleware>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuth();
            app.UseMiddleware<TokenManagerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapAreaControllerRoute(
                //    name: "MyAreaAdmin",
                //    areaName: "Admin",
                //    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapAreaControllerRoute(
                //    name: "MyAreaServices",
                //    areaName: "Services",
                //    pattern: "Services/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllers();

            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "twajd-api/swagger/{documentName}/swagger.json";
                //c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = "twajd-api";
                //c.RoutePrefix = "";
            });


        }
    }
}
