using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {

        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUploadRepository, UploadRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUploadService, UploadService>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("sqlConnection");
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(connectionString));
        }

    }
}
