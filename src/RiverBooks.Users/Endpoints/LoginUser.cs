using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Endpoints;

public record LoginUserRequest(string Email, string Password);

internal class LoginUser(UserManager<ApplicationUser> userManager) : Endpoint<LoginUserRequest>
{
    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(req.Email);
        if (user is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var isValidPassword = await userManager.CheckPasswordAsync(user, req.Password);
        if (!isValidPassword)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }
        var jwtToken = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = Config["Auth:JwtSecret"]!;
            o.ExpireAt = DateTime.UtcNow.AddYears(1);
            o.User.Claims.Add(("EmailAddress", req.Email));
        });
        await SendAsync(jwtToken);
    }
}