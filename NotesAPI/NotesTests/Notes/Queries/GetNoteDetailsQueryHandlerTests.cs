using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using NotesTests.Common;
using Shouldly;

namespace NotesTests.Notes.Queries;

[Collection("QueryCollection")]
public class GetNoteDetailsQueryHandlerTests
{
    private readonly NotesDbContext _context;
    private readonly IMapper _mapper;

    public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        _context = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetNoteDetailsQuery_Success()
    {
        // Arrange
        var handler = new GetNoteDetailsQueryHandler(_context, _mapper);
        
        // Act
        var result = await handler.Handle(new GetNoteDetailsQuery
        {
            Id = Guid.Parse("0FC46583-5E77-4B38-8B7A-960529915A07"),
            UserId = NotesContextFactory.UserAId
        }, CancellationToken.None);
        
        // Assert
        result.ShouldBeOfType<NoteDetailsVm>();
        result.Title.ShouldBe("Title1");
        result.CreationDate.ShouldBe(DateTime.Today);
    }
}