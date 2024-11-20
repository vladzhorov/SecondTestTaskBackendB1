using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecondTestTaskB1.Db;
using SecondTestTaskB1.Interfaces;
using SecondTestTaskB1.Repositories;
using SecondTestTaskB1.Services;

namespace SecondTestTaskB1.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseOptions>(options => configuration.GetSection(nameof(DatabaseOptions)).Bind(options));

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                options.UseNpgsql(dbOptions.ConnectionString,
                    b => b.MigrationsAssembly("SecondTestTaskB1"));
            });

            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
        public static void AddApplicationModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataAccessDependencies(configuration);
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IExcelService, ExcelService>();

        }
    }
}
