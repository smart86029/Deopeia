namespace Deopeia.Common.Application.Validation;

internal sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(context, cancellationToken);
            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(x =>
                {
                    var states = x
                        .CustomState.GetType()
                        .GetProperties()
                        .ToDictionary(
                            y => y.Name,
                            y => y.GetValue(x.CustomState)?.ToString() ?? string.Empty
                        );

                    return new LocalizableMessage(x.ErrorMessage, states);
                });

                throw new LocalizableMessageException(errorMessages);
            }
        }

        return await next();
    }
}
