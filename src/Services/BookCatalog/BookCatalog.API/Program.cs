using BookCatalog.API.Extensions;

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
        if (!Directory.Exists(Constants.BookCoverPath))
        {
            Directory.CreateDirectory(Constants.BookCoverPath);
        }

        if (!Directory.Exists(Constants.AuthorImagePath))
        {
            Directory.CreateDirectory(Constants.AuthorImagePath);
        }
    }
}