using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Swashbuckle.AspNetCore.SwaggerGen;
using TinkloProblemos.API.Database;
using TinkloProblemos.API.Identity;
using TinkloProblemos.API.Identity.Entities;
using TinkloProblemos.API.Identity.Stores;
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
            services.AddSingleton<IInternetUserRepository, InternetUserRepository>();
            services.AddSingleton<IProblemRepository, ProblemRepository>();
            services.AddSingleton<ITagRepository, TagRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<ITimeSpentRepository, TimeSpentRepository>();
            services.AddSingleton<IPingRepository, PingRepository>();
            services.AddSingleton<IReportsRepository, ReportsRepository>();
            services.AddSingleton<ILocationRepository, LocationRepository>();

            services.AddTransient<IInternetUserService, InternetUserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProblemService, ProblemService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ITimeSpentService, TimeSpentService>();
            services.AddTransient<IPingService, PingService>();
            services.AddTransient<IReportsService, ReportsService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<INotificationService, NotificationService>();

            string connectionString = Configuration.GetConnectionString("Database");
            services.AddTransient<IDatabaseConnectionService>(e => new DatabaseConnectionService(connectionString));
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            };
        });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                            .AddUserManager<ApplicationUserManager>()
                            .AddRoleManager<ApplicationRoleManager>()
                            .AddSignInManager<ApplicationSignInManager>()
                            .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                var applyApiKeySecurity = new ApplyApiKeySecurity();
                applyApiKeySecurity.Apply(c);

                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "My API",
                    Description = "My First ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Tomas", Email = "tomas.valiunas@ktu.edu", Url = "www.bakalaurinis.lt" }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.DocumentFilter<SecurityRequirementsDocumentFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();          
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
           
        }
    }
    public class SecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument document, DocumentFilterContext context)
        {
            document.Security = new List<IDictionary<string, IEnumerable<string>>>()
            {
                new Dictionary<string, IEnumerable<string>>()
                {
                    { "Bearer", new string[]{ } },
                    { "Basic", new string[]{ } },
                }
            };
        }
    }

    public class ApplyApiKeySecurity : IDocumentFilter, IOperationFilter
    {
        /// <summary>
        /// Token input boxes in the header
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The swagger document.</param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var tokenParameter = new BodyParameter
            {
                Name = "Bearer",
                Description = "Authorization Token",
                @In = "header",
                Required = true
            };


            IList<IDictionary<string, IEnumerable<string>>> security =
                new List<IDictionary<string, IEnumerable<string>>>();
            security.Add(new Dictionary<string, IEnumerable<string>>
            {
                {tokenParameter.Name, new string[0]}
            });

            swaggerDoc.Security = security;
        }


        /// <summary>
        /// Operation-level token input fields
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The operation.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<IParameter>();

            var tokenAttribute =
                context.ApiDescription.ActionAttributes().OfType<AuthorizeAttribute>().FirstOrDefault() ??
                context.ApiDescription.ControllerAttributes().OfType<AuthorizeAttribute>().FirstOrDefault();

            if (tokenAttribute != null)
            {
                operation.Responses.Add("401", new Response { Description = "Authorization token required" });
            }
        }

        public void Apply(SwaggerGenOptions c)
        {
            c.OperationFilter<ApplyApiKeySecurity>();
        }
    }
}
