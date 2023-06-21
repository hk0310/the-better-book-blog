using BookCatalog.API.CQRS.Authors.Commands;
using BookCatalog.API.CQRS.Genres.Commands;
using BookCatalog.API.CQRS.Genres.Queries;
using BookCatalog.API.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookCatalog.API.Controllers;

[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Genre>))]
    [SwaggerOperation("Get All Genres")]
    public async Task<IActionResult> GetAllGenres()
    {
        return Ok(await _mediator.Send(new GetAllGenresQuery()));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Genre))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Add New Genre")]
    public async Task<IActionResult> CreateGenre(CreateGenreCommand command)
    {
        try
        {
            var genre = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Genre))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation("Get Genre By Id")]
    public async Task<IActionResult> GetGenreById(int id)
    {
        var genre = await _mediator.Send(new GetGenreByIdQuery() { Id = id });

        return genre == null ? NotFound("Invalid genre Id") : Ok(genre);
    }

    [HttpDelete("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation("Remove Genre")]
    public async Task<IActionResult> RemoveGenreById(int id)
    {
        var isSuccess = await _mediator.Send(new DeleteGenreByIdCommand() { Id = id });

        return isSuccess ? Ok("The genre has been successfully deleted.") : NotFound("Invalid genre Id");
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Update Genre Info")]
    public async Task<IActionResult> UpdateGenreById(int id, UpdateGenreByIdCommand command)
    {
        if (command.Id != id)
        {
            return BadRequest();
        }

        try
        {
            var isSuccess = await _mediator.Send(command);

            return isSuccess ? Ok("The genre info has been updated") : NotFound("Invalid genre Id");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
