using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtCompany.Models;


namespace ShirtCompany.Filters.ActionFilters
{
    public class Product_ValidateUpdateProductFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var product = context.ActionArguments["product"] as Product;

            if (id.HasValue && product != null && id != product.ProductID)
            {
                context.ModelState.AddModelError("ProductId", "ProductID is not the same as id");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }

        }
    }
}