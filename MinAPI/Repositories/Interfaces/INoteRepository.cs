public interface INoteRepository
{
    Task<List<Note>> GetNotesAsync(string userId);
    Task AddNoteAsync(string userId, Note note);
    Task EditNoteAsync(string userId, int noteId, Note note);
    Task DeleteNoteAsync(string userId, int noteId);
}