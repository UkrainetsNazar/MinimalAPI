using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class Note
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }
}

public class RegisterDto
{
    [Required]
    public string? Username { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
        ErrorMessage = "Password must contain at least 1 uppercase, 1 lowercase letter, 1 number and be at least 6 characters long")]
    public string? Password { get; set; }
}
public record LoginDto(string Email, string Password);
public record NoteDto(string Title, string Text);