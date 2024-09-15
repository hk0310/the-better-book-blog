using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Books.Queries
{
    public class GetBookSearchQuery : IQuery<IEnumerable<Book>>
    {
        public string QueryString { get; set; }
    }

    public class GetBookSearchQueryHandler : IQueryHandler<GetBookSearchQuery, IEnumerable<Book>>
    {
        private readonly IBookCatalogContext _context;

        public GetBookSearchQueryHandler(IBookCatalogContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<Book>> Handle(GetBookSearchQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> books;

            try
            {
                var isbn = new Isbn(request.QueryString);
                books = await _context.Books.Where(b => b.Isbn.Equals(isbn)).ToListAsync();
            }
            catch (ArgumentException ex)
            {
                books = await _context.Books.Where(b => b.Title.Contains(request.QueryString))
                    .Union(_context.Books.Include(b => b.Author).Where(b => b.Author.FirstName.Contains(request.QueryString))).ToListAsync();
            }

            return books;
        }
    }
}
