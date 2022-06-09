namespace VacationManager.API
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;
    using VacationManager.API.Configuration;
    using VacationManager.API.Extensions;
    using VacationManager.Data;

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
                .UseSqlServer(this._configuration["Secrets:ConnectionString"]),
                ServiceLifetime.Transient
            );

            ServiceConfiguration.AddSecretsConfigurations(services, this._configuration);
            ServiceConfiguration.AddScopedConfigurations(services);

            ServiceConfiguration.AddSwaggerConfigurations(services);

            services
                .AddMvc()
                .AddMvcOptions(mvc => mvc.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            ServiceConfiguration.AddCorsConfigurations(services);
            ServiceConfiguration.AddAuthConfigurations(services, this._configuration);
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
