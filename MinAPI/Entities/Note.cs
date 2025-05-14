using Microsoft.AspNetCore.Identity;

public class Note {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }
}

public record RegisterDto(string Username, string Email, string Password);
public record LoginDto(string Username, string Password);
public record NoteDto(string Title, string Text);