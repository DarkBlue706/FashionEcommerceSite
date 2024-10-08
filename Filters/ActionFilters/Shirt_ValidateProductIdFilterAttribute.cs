using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtCompany.Models.Repositories;

namespace ShirtCompany.Filters.ActionFilters
{

    public class Shirt_ValidatateProductIdFiltureAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var productId = context.ActionArguments["id"] as int?;
            if (productId.HasValue)
            {
                if (productId.Value <= 0)
                {
               
                    context.ModelState.AddModelError("ProductId", "ProductId is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (!ShirtRepository.ShirtExists(productId.Value))
                {
                    context.ModelState.AddModelError("ProductId", "Product does not exist.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status =  StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);

                }
                else
                {
                // No ProductId provided
                context.ModelState.AddModelError("ProductId", "ProductId is required.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }

}