using Ardalis.Result;
using MediatR;

namespace RiverBooks.Users.Contracts;

public record AddressDetailsByIdQuery(Guid AddressId) : IRequest<Result<AddressDetails>>;
