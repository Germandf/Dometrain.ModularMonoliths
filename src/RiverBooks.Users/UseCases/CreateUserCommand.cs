using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RiverBooks.EmailSending.Contracts;
using RiverBooks.Users.Domain;

namespace RiverBooks.Users.UseCases;

internal record CreateUserCommand(string Email, string Password) : IRequest<Result>;

internal class CreateUserHandler(
    UserManager<ApplicationUser> userManager,
    IMediator mediator)
    : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        var result = await userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded is false)
            return Result.Error(new ErrorList(result.Errors.Select(e => e.Description).ToArray()));

        var sendEmailCommand = new SendEmailCommand
        {
            To = request.Email,
            From = "donotreply@test.com",
            Subject = "Welcome to River Books!",
            Body =
                $"Hello {newUser.UserName},\n\n" +
                $"Thank you for registering with River Books! We're excited to have you on board.\n\n" +
                $"Best regards,\n" +
                $"River Books Team"
        };

        _ = await mediator.Send(sendEmailCommand, cancellationToken);

        return Result.Success();
    }
}
