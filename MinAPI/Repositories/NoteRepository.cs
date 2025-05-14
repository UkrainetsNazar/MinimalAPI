using Microsoft.EntityFrameworkCore;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _dbContext;

    public NoteRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddNoteAsync(string userId, Note note, CancellationToken cancellationToken)
    {
        try
        {
            note.UserId = userId;
            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new Exception("Failed to add note");
        }
    }

    public async Task DeleteNoteAsync(string userId, int noteId, CancellationToken cancellationToken)
    {
        try
        {
            var existingNote = await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId, cancellationToken)
            ?? throw new Exception($"Note with id {noteId} doesn`t exist");

            _dbContext.Notes.Remove(existingNote);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            throw new Exception("Failed to delete note");
        }
    }

    public async Task EditNoteAsync(string userId, int noteId, Note note, CancellationToken cancellationToken)
    {
        try{
            var existingNote = await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId, cancellationToken)
            ?? throw new Exception($"Note with id {noteId} doesn`t exist");

            existingNote.Text = note.Text;
            existingNote.Title = note.Title;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch{
            throw new Exception("Failed to edit note");
        }
    }

    public async Task<List<Note>> GetNotesAsync(string userId, CancellationToken cancellationToken)
    {
        try{
            return await _dbContext.Notes.Where(n => n.UserId == userId).ToListAsync(cancellationToken);
        }
        catch{
            throw new Exception("Failed to get notes");
        }
    }
}