using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ShirtCompany.Models;

namespace ShirtCompany.Filters.ExceptionFilters
{
    public class Product_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ProductDBContext _context;

        // Inject DbContext to interact with the database
        public Product_HandleUpdateExceptionsFilterAttribute(ProductDBContext context)
        {
            _context = context;
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strProductId = context.RouteData.Values["id"] as string;

            if (int.TryParse(strProductId, out int productId))
            {
                // Check if the product exists in the database
                var productExists = _context.Product.Any(p => p.ProductID == productId);

                if (!productExists)
                {
                    context.ModelState.AddModelError("ProductId", "Product does not exist anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}
