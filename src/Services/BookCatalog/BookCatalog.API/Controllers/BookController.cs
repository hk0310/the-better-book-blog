using BookCatalog.API.CQRS.Authors.Commands;
using BookCatalog.API.CQRS.Books.Commands;
using BookCatalog.API.CQRS.Books.Queries;
using BookCatalog.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookCatalog.API.Controllers;

[ApiController]
[Route("api/books")]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Book>))]
    [SwaggerOperation("GetAllBooks")]
    public async Task<IActionResult> GetAllBooks()
    {
        return Ok(await _mediator.Send(new GetAllBooksQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add New Book")]
    public async Task<IActionResult> CreateBook(CreateBookCommand command)
    {
        try
        {
            var book = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation("Get Book By Id")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery() { Id = id });

        return book == null ? NotFound("Invalid book Id") : Ok(book);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Remove Book")]
    public async Task<IActionResult> RemoveBookById(int id)
    {
        var isSuccess = await _mediator.Send(new DeleteBookByIdCommand { Id = id });

        return isSuccess ? Ok("The book has been successfully deleted.") : NotFound("Invalid book Id");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Update Book Info")]
    public async Task<IActionResult> UpdateBookById(int id, UpdateBookByIdCommand command)
    {
        if (command.Id != id)
        {
            return BadRequest();
        }

        try
        {
            var isSuccess = await _mediator.Send(command);

            return isSuccess ? Ok("The book info has been updated") : NotFound("Invalid book Id");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}/image")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add Book Cover")]
    [RequestSizeLimit(300_000)]
    public async Task<IActionResult> CreateBookCoverById(int id, CreateBookCoverByIdCommand command)
    {
        if (command.Id != id)
        {
            return BadRequest();
        }

        try
        {
            var isSuccess = await _mediator.Send(command);

            return isSuccess ? Accepted("Book cover is updated") : NotFound("Invalid book Id");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
