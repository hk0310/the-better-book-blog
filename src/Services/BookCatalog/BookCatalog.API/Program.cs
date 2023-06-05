using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BookCatalog.API.Extensions;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace BookCatalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

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

        builder.Services.ConfigureServices(builder.Configuration);

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookCatalog.API");
        });

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}