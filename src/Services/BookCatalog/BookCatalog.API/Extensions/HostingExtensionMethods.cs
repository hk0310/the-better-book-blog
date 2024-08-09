using BookCatalog.API.CQRS.Authentication;
using BookCatalog.API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace BookCatalog.API.Extensions;

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

        builder.Services.AddDbContext<BookCatalogContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure());
        });

        builder.Services.AddScoped<IBookCatalogContext>(provider => provider.GetService<BookCatalogContext>());

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IUserAccessor, UserAccessor>();

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = builder.Configuration["IdentityServer:AuthorityUrl"];
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false
                };
                options.MapInboundClaims = false;
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ReadScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "library.read");
            });
            options.AddPolicy("ModifyScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "library.modify");
            });
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
