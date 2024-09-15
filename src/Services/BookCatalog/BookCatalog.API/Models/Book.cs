using BookCatalog.API.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookCatalog.API.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Synopsis { get; set; }

    public int PageCount { get; set; }

    [JsonConverter(typeof(IsbnConverter))]
    public Isbn Isbn { get; set; }

    public DateOnly PublishDate { get; set; }

    public string CoverImagePath { get; set; }

    public int AuthorId { get; set; }

    public Author Author { get; set; }

    public ICollection<Genre> Genres { get; set; }
}

public class Isbn
{
    public string Number { get; }

    public Isbn(string number)
    {
        if (Isbn.IsValidIsbn(Isbn.NormalizeIsbn(number)))
        {
            Number = number;
        }
        else
        {
            throw new ArgumentException("The provided ISBN is invalid");
        }
    }

    public override string ToString()
    {
        return Number;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if(!(obj is Isbn))
        {
            return false;
        }

        return this.Number.Equals(((Isbn)obj).Number);
    }

    public static string NormalizeIsbn(string isbn) 
    {
        return isbn.Replace("-", "");
    }

    public static bool IsValidIsbn(string isbn)
    {
        if (isbn.Length != 13 && isbn.Length != 10)
        {
            return false;
        }

        int sum = 0;

        if (isbn.Length == 10)
        {
            int coefficient = 10;
            for (int i = 0; i < 10; i++)
            {
                int num;

                if (i == 9)
                {
                    if (isbn[i] == 'X')
                    {
                        sum += 10;
                        continue;
                    }
                }

                if (int.TryParse(isbn[i].ToString(), out num))
                {
                    sum += num * (coefficient - i);
                }
                else
                {
                    return false;
                }
            }

            if (sum % 11 != 0)
            {
                return false;
            }
        }
        else
        {
            int coefficient = 1;
            foreach (char c in isbn)
            {
                int num;
                if (int.TryParse(c.ToString(), out num))
                {
                    sum += num * coefficient;
                }
                else
                {
                    return false;
                }
                coefficient = coefficient == 1 ? 3 : 1;

                if (sum % 10 != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }
}