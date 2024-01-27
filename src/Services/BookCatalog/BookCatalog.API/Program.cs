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

        var app = builder
            .ConfigureServices()
            .ConfigurePipelines();

        SetUpDirectory();

        app.Run();
    }

    public static void SetUpDirectory()
    {
        if(!Directory.Exists(Constants.BookCoverPath)) 
        {
            Directory.CreateDirectory(Constants.BookCoverPath);
        }

        if (!Directory.Exists(Constants.AuthorImagePath))
        {
            Directory.CreateDirectory(Constants.AuthorImagePath);
        }
    }
}