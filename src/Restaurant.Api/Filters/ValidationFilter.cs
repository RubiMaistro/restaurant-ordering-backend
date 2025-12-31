using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Restaurant.Api.Filters
{
    public class ValidationFilter<T> : IAsyncActionFilter
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var argument = context.ActionArguments.Values
                .OfType<T>()
                .FirstOrDefault();

            if (argument is not null)
            {
                var validationResult = await _validator.ValidateAsync(argument);

                if (!validationResult.IsValid)
                {
                    context.Result = new BadRequestObjectResult(
                        validationResult.Errors.Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        })
                    );
                    return;
                }
            }

            await next();
        }
    }
}
