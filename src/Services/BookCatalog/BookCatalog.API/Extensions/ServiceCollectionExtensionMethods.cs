using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Extensions
{
    public static class ServiceCollectionExtensionMethods
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<BookCatalogContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IBookCatalogContext>(provider => provider.GetService<BookCatalogContext>());

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            }); 
        }
    }
}
