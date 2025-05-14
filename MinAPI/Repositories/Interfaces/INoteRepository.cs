public interface INoteRepository
{
    Task<List<Note>> GetNotesAsync(string userId, CancellationToken cancellationToken);
    Task AddNoteAsync(string userId, Note note, CancellationToken cancellationToken);
    Task EditNoteAsync(string userId, int noteId, Note note, CancellationToken cancellationToken);
    Task DeleteNoteAsync(string userId, int noteId, CancellationToken cancellationToken);
}