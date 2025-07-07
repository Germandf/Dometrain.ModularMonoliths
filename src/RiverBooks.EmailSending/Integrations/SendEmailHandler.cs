using Ardalis.Result;
using MediatR;
using RiverBooks.EmailSending.Contracts;

namespace RiverBooks.EmailSending.Integrations;

internal class SendEmailHandler(IEmailSender emailSender)
    : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await emailSender.SendEmailAsync(
            request.To,
            request.From,
            request.Subject,
            request.Body,
            cancellationToken);

        return Guid.Empty;
    }
}
