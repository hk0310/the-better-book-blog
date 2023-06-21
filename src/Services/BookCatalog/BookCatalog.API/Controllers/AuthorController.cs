using Microsoft.AspNetCore.Mvc;
using MediatR;
using BookCatalog.API.CQRS.Authors.Queries;
using BookCatalog.API.CQRS.Authors.Commands;
using BookCatalog.API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace BookCatalog.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Author>))]
    [SwaggerOperation("Get All Authors")]
    public async Task<IActionResult> GetAllAuthors()
    {
        return Ok(await _mediator.Send(new GetAllAuthorsQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Author))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add New Author")]
    public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
    {
        try
        {
            var author = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Author))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation("Get Author By Id")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery { Id = id });

        return author == null ? NotFound("Invalid author Id") : Ok(author);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Remove Author")]
    public async Task<IActionResult> RemoveAuthorById(int id)
    {
        var isSuccess = await _mediator.Send(new DeleteAuthorByIdCommand { Id = id });

        return isSuccess ? Ok("The author has been successfully deleted.") : NotFound("Invalid author Id");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Update Author Info")]
    public async Task<IActionResult> UpdateAuthorById(int id, UpdateAuthorByIdCommand command)
    {
        if(command.Id != id)
        {
            return BadRequest();
        }

        try
        {
            var isSuccess = await _mediator.Send(command);

            return isSuccess ? Ok("The author info has been updated") : NotFound("Invalid author Id");
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id}/image")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add Author Image")]
    [RequestSizeLimit(300_000)]
    public async Task<IActionResult> UpdateAuthorImageById(int id, UpdateAuthorImageByIdCommand command)
    {
        if (command.Id != id)
        {
            return BadRequest();
        }

        try
        {
            var isSuccess = await _mediator.Send(command);

            return isSuccess ? Accepted("Author's image is updated") : NotFound("Invalid book Id");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
