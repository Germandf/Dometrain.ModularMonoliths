using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.UseCases;

public record AddAddressCommand(
    string EmailAddress,
    string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country) : IRequest<Result>;

internal class AddAddressHandler(IApplicationUserRepository userRepository)
    : IRequestHandler<AddAddressCommand, Result>
{
    public async Task<Result> Handle(AddAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);
        if (user is null)
            return Result.Unauthorized();

        var address = new Address(
            request.Street1,
            request.Street2,
            request.City,
            request.State,
            request.PostalCode,
            request.Country);

        user.AddAddress(address);
        await userRepository.SaveChangesAsync();
        return Result.Success();
    }
}
