using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using RiverBooks.Users.UseCases;

namespace RiverBooks.Users.Endpoints;

public record CreateUserRequest(string Email, string Password);

internal class CreateUser(IMediator mediator) : Endpoint<CreateUserRequest>
{
    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var command = new CreateUserCommand(req.Email, req.Password);
        var result = await mediator.Send(command, ct);
        if (result.IsSuccess is false)
            await SendResultAsync(result.ToMinimalApiResult());
        else
            await SendOkAsync();
    }
}
