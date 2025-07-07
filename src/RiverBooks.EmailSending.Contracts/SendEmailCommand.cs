using Ardalis.Result;
using MediatR;

namespace RiverBooks.EmailSending.Contracts;

public class SendEmailCommand : IRequest<Result<Guid>>
{
    public required string To { get; set; }
    public required string From { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}
