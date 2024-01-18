using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers;

[ApiVersion("1.0")]
[ApiVersion("2.0")]
[Produces("application/json")]
[Route("api/{version:apiVersion}/[controller]/[action]")]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;

    public NoteController(IMapper mapper) => _mapper = mapper;
    
    /// <summary>
    /// Gets the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /Note/GetAll
    /// </remarks>
    /// <returns>NoteListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If user is unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery { UserId = UserId };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Get the note by id
    /// <remarks>
    /// Sample request:
    /// GET /Note/Get/B482A077-475A-4157-BDAD-881E18602D1E
    /// </remarks>
    /// </summary>
    /// <param name="id">Note id (Guid)</param>
    /// <returns>NoteDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If user is unauthorized</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery { UserId = UserId, Id = id };

        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    /// <summary>
    /// Creates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /Note/Create
    /// {
    ///     title: "title"
    ///     details: "details"
    /// }
    /// </remarks>
    /// <param name="createNoteDto">CreateNoteDto Object</param>
    /// <returns>Returns id (Guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If user is unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;

        var noteId = await Mediator.Send(command);

        return Ok(noteId);
    }

    /// <summary>
    /// Updates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /Note/Update
    /// {
    ///     title: "new title"
    /// }
    /// </remarks>
    /// <param name="updateNoteDto">UpdateNoteDto Object</param>
    /// <returns>NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If user is unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = UserId;

        await Mediator.Send(command);

        return NoContent();
    }

    /// <summary>
    /// Deletes the note by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /Note/Delete/7F30204E-2CED-4975-9A5C-6FE114C85AAA
    /// </remarks>
    /// <param name="id">Note id (Guid)</param>
    /// <returns>NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If user is unauthorized</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand { Id = id, UserId = UserId };

        await Mediator.Send(command);

        return NoContent();
    }
}