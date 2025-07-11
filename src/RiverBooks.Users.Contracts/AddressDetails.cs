﻿namespace RiverBooks.Users.Contracts;

public record AddressDetails(
    Guid UserId,
    Guid Id,
    string Street1,
    string Street2,
    string City,
    string State,
    string PostalCode,
    string Country);
