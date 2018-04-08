using Identity.Dapper;
using Identity.Dapper.Entities;
using Identity.Dapper.PostgreSQL.Connections;
using Identity.Dapper.PostgreSQL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TinkloProblemos.API.Database;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;
using TinkloProblemos.API.Models;
using TinkloProblemos.API.Services;

namespace TinkloProblemos.API
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
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICategoryService, CategoryService>();

            services.ConfigureDapperConnectionProvider<PostgreSqlConnectionProvider>(Configuration.GetSection("DapperIdentity"))
                .ConfigureDapperIdentityCryptography(Configuration.GetSection("DapperIdentityCryptography"));

            services.AddIdentity<CustomUser, CustomRole>(x =>
                {
                    x.Password.RequireDigit = false;
                    x.Password.RequiredLength = 1;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireUppercase = false;
                })
                .AddDapperIdentityFor<PostgreSqlConfiguration>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "My First ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Tomas", Email = "tomas.valiunas@ktu.edu", Url = "www.bakalaurinis.lt" }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
