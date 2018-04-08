using Daarto.IdentityProvider.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TinkloProblemos.API.Database;
using TinkloProblemos.API.Identity;
using TinkloProblemos.API.Identity.Entities;
using TinkloProblemos.API.Interfaces.Repositories;
using TinkloProblemos.API.Interfaces.Services;
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

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                              .AddUserManager<ApplicationUserManager>()
                              .AddRoleManager<ApplicationRoleManager>()
                              .AddSignInManager<ApplicationSignInManager>()
                              .AddDefaultTokenProviders();


            string connectionString = Configuration.GetConnectionString("Database");

            // Configure custom services to be used by the framework.
            services.AddTransient<IDatabaseConnectionService>(e => new DatabaseConnectionService(connectionString));
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();


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
