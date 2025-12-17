using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Restaurant.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        private readonly ServiceProvider _serviceProvider;

        public ValidationFilter(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                var validatorType = typeof(IValidator<>)
                    .MakeGenericType(argument.GetType());

                var validator = _serviceProvider.GetService(validatorType);
                if (validator is null) continue;

                var validationContext = new ValidationContext<object>(argument);
                var result = ((IValidator<object>)validator)
                    .Validate(validationContext);

                if (!result.IsValid)
                {
                    context.Result = new BadRequestObjectResult(
                        result.Errors.Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        }));
                    return;
                }
            }

            await next();
        }
    }
}
