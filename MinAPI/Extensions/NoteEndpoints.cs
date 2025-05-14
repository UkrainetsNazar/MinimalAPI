using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public static class NoteEndpoints
{
    public static void MapNoteEndpoints(this WebApplication app)
    {
        app.MapGet("/notes", [Authorize] async (HttpContext ctx, INoteRepository repository) =>
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Results.Ok(await repository.GetNotesAsync(userId!));
        });

        app.MapPost("/notes", [Authorize] async (HttpContext ctx, INoteRepository repository, NoteDto dto) =>
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await repository.AddNoteAsync(userId!, new Note { Title = dto.Title, Text = dto.Text });
            return Results.Ok();
        });

        app.MapPut("/notes/{id}", [Authorize] async (HttpContext ctx, INoteRepository repository, int id, NoteDto dto) =>
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await repository.EditNoteAsync(userId!, id, new Note { Title = dto.Title, Text = dto.Text });
            return Results.Ok();
        });

        app.MapDelete("/notes/{id}", [Authorize] async (HttpContext ctx, INoteRepository repository, int id) =>
        {
            var userId = ctx.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await repository.DeleteNoteAsync(userId!, id);
            return Results.Ok();
        });
    }
}
