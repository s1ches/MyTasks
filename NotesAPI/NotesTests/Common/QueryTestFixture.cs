using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistence;

namespace NotesTests.Common;

public class QueryTestFixture : IDisposable
{
    public readonly NotesDbContext Context;
    public readonly IMapper Mapper;

    public QueryTestFixture()
    {
        Context = NotesContextFactory.Create();

        var configureBuilder = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
        });

        Mapper = configureBuilder.CreateMapper();
    }

    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}