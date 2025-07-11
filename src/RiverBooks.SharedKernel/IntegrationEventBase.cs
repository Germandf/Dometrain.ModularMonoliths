﻿using MediatR;

namespace RiverBooks.SharedKernel;

public abstract record IntegrationEventBase : INotification
{
    public DateTimeOffset DateTimeOffset { get; } = DateTimeOffset.UtcNow;
}
