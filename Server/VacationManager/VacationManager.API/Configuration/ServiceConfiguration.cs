namespace VacationManager.API.Configuration
{
    using System;
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using VacationManager.API.Configuration.Roles;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Business.Services.AuthService;
    using VacationManager.Business.Services.Notification;
    using VacationManager.Core.Utility;
    using VacationManager.Data.Contracts;
    using VacationManager.Data.Repositories;
    using VacationManager.Domain.Constants;
    using VacationManager.Domain.Models;
    using VacationManager.Domain.Models.Configuration;

    public static class ServiceConfiguration
    {
        public static IServiceCollection AddSecretsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Secrets>(configuration.GetSection("Secrets"));
            services.Configure<BusinessEmailCredentials>(configuration.GetSection("Credentials"));

            return services;
        }

        public static IServiceCollection AddScopedConfigurations(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConfirmRegistrationCodeRepository, ConfirmRegistrationCodeRepository>();

            services.AddScoped<IJwtUtils, JwtUtils>();

            return services;
        }

        public static IServiceCollection AddAuthConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Secrets:JwtSecret"]);

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

            return services;
        }

        public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VacationManager.API", Version = "v1" });
            });

            return services;
        }

        public static IServiceCollection AddCorsConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }
    }
}
