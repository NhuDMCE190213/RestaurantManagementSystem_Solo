using System.Reflection;
using FluentValidation;
using MediatR;
using RestaurantManagement.Application.Common;
using RestaurantManagement.Domain.Constants.ErrorCodes;

namespace RestaurantManagement.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators != null && _validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    var errorMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));

                    // Build a Result<T> failure dynamically: TResponse is expected to be Result<T>
                    var responseType = typeof(TResponse);
                    if (responseType.IsGenericType && responseType.GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var genericArg = responseType.GetGenericArguments()[0];
                        var resultType = typeof(Result<>).MakeGenericType(genericArg);
                        var failureMethod = resultType.GetMethod("Failure", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string), typeof(string) }, null);
                        if (failureMethod != null)
                        {
                            var failure = failureMethod.Invoke(null, new object[] { CommonErrorCodes.ValidationError, errorMessage });
                            return (TResponse)failure!;
                        }
                    }

                    // If we cannot construct a typed failure, throw validation exception as fallback
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
