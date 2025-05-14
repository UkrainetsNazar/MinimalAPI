using Microsoft.EntityFrameworkCore;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _dbContext;

    public NoteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddNoteAsync(string userId, Note note)
    {
        try
        {
            note.UserId = userId;
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Failed to add note");
        }
    }

    public async Task DeleteNoteAsync(string userId, int noteId)
    {
        try
        {
            var existingNote = await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId)
            ?? throw new Exception($"Note with id {noteId} doesn`t exist");

            _dbContext.Notes.Remove(existingNote);
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new Exception("Failed to delete note");
        }
    }

    public async Task EditNoteAsync(string userId, int noteId, Note note)
    {
        try{
            var existingNote = await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId)
            ?? throw new Exception($"Note with id {noteId} doesn`t exist");

            existingNote.Text = note.Text;
            existingNote.Title = note.Title;
            await _dbContext.SaveChangesAsync();
        }
        catch{
            throw new Exception("Failed to edit note");
        }
    }

    public async Task<List<Note>> GetNotesAsync(string userId)
    {
        try{
            return await _dbContext.Notes.Where(n => n.UserId == userId).ToListAsync();
        }
        catch{
            throw new Exception("Failed to get notes");
        }
    }
}