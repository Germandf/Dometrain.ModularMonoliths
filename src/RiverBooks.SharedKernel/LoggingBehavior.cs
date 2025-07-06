using Ardalis.GuardClauses;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RiverBooks.SharedKernel;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        Guard.Against.Null(request);

        logger.LogInformation("Handling {RequestName} with request: {@Request}",
            typeof(TRequest).Name, request);

        var sw = Stopwatch.StartNew();
        var response = await next();
        sw.Stop();

        logger.LogInformation("Handled {RequestName} with response: {@Response} in {ElapsedMilliseconds}ms",
            typeof(TRequest).Name, response, sw.ElapsedMilliseconds);

        return response;
    }
}
