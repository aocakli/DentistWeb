using ValidationException = DentOnline.Application.Utilities.Exceptions.ValidationException;

namespace DentOnline.Application.BehaviorPipelines.Validation;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any() is false)
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResult =
            await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var validationFailures = validationResult.SelectMany(_validationResult => _validationResult.Errors)
            .Where(_validationFailure => _validationFailure is not null);

        if (validationFailures.Any())
            throw new ValidationException(validationFailures);

        return await next();
    }
}