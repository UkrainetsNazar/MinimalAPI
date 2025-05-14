using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/register", async (UserManager<IdentityUser> userManager, RegisterDto dto) =>
        {
            var user = new IdentityUser { UserName = dto.Username, Email = dto.Email };
            var result = await userManager.CreateAsync(user, dto.Password);

            return result.Succeeded ? Results.Ok("User registered") : Results.BadRequest(result.Errors);
        });

        app.MapPost("/login", async (UserManager<IdentityUser> userManager, IConfiguration config, LoginDto dto) =>
        {
            var user = await userManager.FindByNameAsync(dto.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
                return Results.Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Results.Ok(new { token = tokenString });
        });
    }
}
