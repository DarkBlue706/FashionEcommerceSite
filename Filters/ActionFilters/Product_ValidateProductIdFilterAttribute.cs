using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace ShirtCompany.Filters.ActionFilters
{
    public class Product_ValidateProductIdFilterAttribute : ActionFilterAttribute
    {
        private readonly ProductDBContext _context;

        // Injecting the DbContext to interact with the database
        public Product_ValidateProductIdFilterAttribute(ProductDBContext context)
        {
            _context = context;
        }

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
                else
                {
                    // Check if the product exists in the database
                    var productExists = _context.Product.Any(p => p.ProductID == productId.Value);
                    if (!productExists)
                    {
                        context.ModelState.AddModelError("ProductId", "Product does not exist.");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetails);
                    }
                }
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
