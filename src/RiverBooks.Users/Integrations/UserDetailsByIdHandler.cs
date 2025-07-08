using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;
using RiverBooks.Users.UseCases;

namespace RiverBooks.Users.Integrations;

internal class UserDetailsByIdHandler(IMediator mediator)
    : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
    public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(request.UserId);
        var result = await mediator.Send(query, cancellationToken);
        return result.Map(u => new UserDetails(u.Id, u.Email));
    }
}
