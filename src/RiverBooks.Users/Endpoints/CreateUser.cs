using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users.Endpoints;

public record CreateUserRequest(string Email, string Password);

internal class CreateUser(UserManager<ApplicationUser> userManager) : Endpoint<CreateUserRequest>
{
    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var newUser = new ApplicationUser
        {
            Email = req.Email,
            UserName = req.Email
        };
        await userManager.CreateAsync(newUser, req.Password);
        await SendOkAsync();
    }
}
