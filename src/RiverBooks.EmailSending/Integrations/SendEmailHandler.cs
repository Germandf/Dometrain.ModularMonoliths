using Ardalis.Result;
using MediatR;
using RiverBooks.EmailSending.Contracts;

namespace RiverBooks.EmailSending.Integrations;

internal class SendEmailHandler(IOutboxService outboxService) : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var email = new EmailOutbox
        {
            To = request.To,
            From = request.From,
            Subject = request.Subject,
            Body = request.Body,
        };

        await outboxService.QueueEmail(email);

        return email.Id;
    }
}
