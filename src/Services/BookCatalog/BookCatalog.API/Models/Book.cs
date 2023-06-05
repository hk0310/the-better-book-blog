namespace BookCatalog.API.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Synopsis { get; set; }

    public int PageCount { get; set; }

    public DateTime DatePublished { get; set; }

    public string CoverImagePath { get; set; }

    public int AuthorId { get; set; }   

    public Author Author { get; set; }

    public ICollection<Genre> Genres { get; set; } 
}
