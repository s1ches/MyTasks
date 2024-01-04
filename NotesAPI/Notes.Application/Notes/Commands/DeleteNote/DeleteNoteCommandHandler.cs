using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDbContext _dbContext;

    public DeleteNoteCommandHandler(INotesDbContext dbContext) 
        =>_dbContext = dbContext;
    

    public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);

        if (entity is null || entity.UserId != request.UserId)
            throw new NotFoundException(nameof(Note), request.Id);

        _dbContext.Notes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}