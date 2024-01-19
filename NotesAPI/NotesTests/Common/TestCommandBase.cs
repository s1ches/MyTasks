using Notes.Persistence;

namespace NotesTests.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly NotesDbContext Context;

    protected TestCommandBase()
    {
        Context = NotesContextFactory.Create();
    }

    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}