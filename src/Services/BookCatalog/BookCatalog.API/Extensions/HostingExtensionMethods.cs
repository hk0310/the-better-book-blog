using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace BookCatalog.API.Extensions
{
    public static class HostingExtensionMethods
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(o =>
            o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book Catalog API",
                    Description = "A microservice responsible for managing a book catalog, written using ASP.NET Core.",
                    Contact = new OpenApiContact
                    {
                        Name = "Khai Nguyen",
                        Email = "hoangkhai.nt@outlook.com"
                    }
                });
            });

            builder.Services.AddDbContext<BookCatalogContext>(options => {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure());
            });

            builder.Services.AddScoped<IBookCatalogContext>(provider => provider.GetService<BookCatalogContext>());

            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipelines(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookCatalog.API");
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
