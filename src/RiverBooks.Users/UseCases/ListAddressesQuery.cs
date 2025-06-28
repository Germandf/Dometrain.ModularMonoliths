using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Endpoints;

namespace RiverBooks.Users.UseCases;

public record ListAddressesQuery(
    string EmailAddress)
    : IRequest<Result<List<UserAddressDto>>>;

internal class ListAddressesHandler(IApplicationUserRepository userRepository)
    : IRequestHandler<ListAddressesQuery, Result<List<UserAddressDto>>>
{
    public async Task<Result<List<UserAddressDto>>> Handle(ListAddressesQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);
        if (user is null)
            return Result.Unauthorized();

        return user.Addresses
            .Select(address => new UserAddressDto(
                address.Id,
                address.Address.Street1,
                address.Address.Street2,
                address.Address.City,
                address.Address.State,
                address.Address.PostalCode,
                address.Address.Country))
            .ToList();
    }
}
