using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly INotesDbContext _dbContext;
    
    public UpdateNoteCommandHandler(INotesDbContext dbContext)
        => _dbContext = dbContext;
    

    public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

        if (entity is null || entity.UserId != request.UserId)
            throw new NotFoundException(nameof(Note), request.Id);

        entity.Title = request.Title;
        entity.Details = request.Details;
        entity.EditDate = DateTime.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}