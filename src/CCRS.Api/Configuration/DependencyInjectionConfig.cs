using CCRS.Api.Extensions;
using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Notifications;
using CCRS.Business.Services;
using CCRS.Data.Context;
using CCRS.Data.Repository;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CCRS.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IPatientService, PatientService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            // Obtém as configurações de email do appsettings.json
            var emailSettingsSection = configuration.GetSection("EmailSettings");
            services.Configure<EmailSettings>(emailSettingsSection);

            // Registra o EmailSender e IEmailSender como serviços
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            //Fabrica 
            services.AddScoped<UserRoleFactory>();  // Registrando a fábrica para injeção
            services.AddScoped<IUserRoleService, DoctorRoleService>();  // Se quiser injetar diretamente um serviço específico
            services.AddScoped<IUserRoleService, PatientRoleService>(); // Para outro tipo de serviço



            return services;
        }
    }
}
