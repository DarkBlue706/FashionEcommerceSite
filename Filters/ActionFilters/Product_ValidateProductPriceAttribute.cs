using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtCompany.Models;

namespace ShirtCompany.Filters.ActionFilters
{
    public class Product_ValidateProductPriceFilter : ActionFilterAttribute
    {
        private readonly ProductDBContext _productContext;

        public Product_ValidateProductPriceFilter(ProductDBContext productContext)
        {
            _productContext = productContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Try to get the productId from the action arguments safely
            if (context.ActionArguments.TryGetValue("productId", out var productIdObj) && productIdObj is int productId)
            {
                // Fetch the product from the database
                var product = _productContext.Product.Find(productId);

                if (product == null)
                {
                    context.Result = new NotFoundResult();  // If product is not found
                    return;
                }

                // Check if the price is null
                if (product.Price == null)
                {
                    context.Result = new BadRequestObjectResult("Product price is not available.");
                    return;
                }

                // Optionally, you can set a default price if it's null
                // product.Price = product.Price ?? 0;  // Uncomment if needed
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid product ID.");
                return;
            }

            base.OnActionExecuting(context);  // Call the base method to allow the action to execute
        }
    }
}
