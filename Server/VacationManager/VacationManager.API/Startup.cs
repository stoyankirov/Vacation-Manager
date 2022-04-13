namespace VacationManager.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Reflection;
    using System.Text;
    using VacationManager.API.Configuration;
    using VacationManager.API.Configuration.Roles;
    using VacationManager.API.Extensions;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Business.Services.AuthService;
    using VacationManager.Business.Services.Notification;
    using VacationManager.Core.Utility;
    using VacationManager.Data;
    using VacationManager.Data.Contracts;
    using VacationManager.Data.Repositories;
    using VacationManager.Domain.Constants;
    using VacationManager.Domain.Models;
    using VacationManager.Domain.Models.Configuration;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _assemblyName;
        private readonly string _assemblyVersion;
        private readonly string _applicationBaseDirectory;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            _assemblyName = assemblyName.Name;
            _assemblyVersion = $"v{assemblyName.Version?.Major ?? 1}";
            _applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<VacationManagerContext>(options => options
                .UseSqlServer(this._configuration["Secrets:ConnectionString"])
            );

            services.Configure<Secrets>(this._configuration.GetSection("Secrets"));
            services.Configure<BusinessEmailCredentials>(this._configuration.GetSection("Credentials"));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConfirmRegistrationCodeRepository, ConfirmRegistrationCodeRepository>();

            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VacationManager.API", Version = "v1" });
            });

            services
                .AddMvc()
                .AddMvcOptions(mvc => mvc.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            // Cors configuration
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var key = Encoding.UTF8.GetBytes(this._configuration["Secrets:JwtSecret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Policy based authorization
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy(AuthenticationPolicy.User, policy =>
                {
                    policy.AddRequirements(new UserRequirement(true));
                });
                configure.AddPolicy(AuthenticationPolicy.Admin, policy =>
                {
                    policy.AddRequirements(new AdminRequirement(true));
                });
                configure.AddPolicy(AuthenticationPolicy.Owner, policy =>
                {
                    policy.AddRequirements(new OwnerRequirement(true));
                });
            });

            services.AddSingleton<IAuthorizationHandler, UserRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, OwnerRequirementHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationManager.API v1"));
            }

            app.UseMiddleware<JwtMiddleware>();

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
