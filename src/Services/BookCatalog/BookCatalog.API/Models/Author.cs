using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models;

public class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Introduction { get; set; }

    public DateTime DateOfBirth { get; set; }

    public Uri Website { get; set; }

    public string Twitter { get; set; }

    public string ImagePath { get; set; }

    public ICollection<Book> Books { get; set; }
}
