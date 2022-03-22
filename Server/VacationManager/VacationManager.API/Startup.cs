namespace VacationManager.API
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Reflection;
    using System.Text;
    using System.Text.Json;
    using VacationManager.API.Extensions;
    using VacationManager.Business.Contracts.Services;
    using VacationManager.Business.Services.AuthService;
    using VacationManager.Core.Constants;
    using VacationManager.Data;
    using VacationManager.Data.Contracts;
    using VacationManager.Data.Repositories;

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

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VacationManager.API", Version = "v1" });
            });

            // Cors configuration
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationManager.API v1"));
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
