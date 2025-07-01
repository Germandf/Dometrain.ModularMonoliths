using Ardalis.Result;
using MediatR;
using RiverBooks.Users.Contracts;

namespace RiverBooks.Users.Integrations;

internal class AddressDetailsByIdHandler(IReadOnlyUserAddressRepository userAddressRepository)
    : IRequestHandler<AddressDetailsByIdQuery, Result<AddressDetails>>
{
    public async Task<Result<AddressDetails>> Handle(AddressDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var address = await userAddressRepository.GetByIdAsync(request.AddressId);
        if (address is null)
            return Result.NotFound();

        var addressDetails = new AddressDetails(
            Guid.Parse(address.UserId),
            request.AddressId,
            address.Address.Street1,
            address.Address.Street2,
            address.Address.City,
            address.Address.State,
            address.Address.PostalCode,
            address.Address.Country);
        return addressDetails;
    }
}
