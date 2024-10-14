using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtCompany.Models;

namespace ShirtCompany.Filters.ActionFilters;
    public class Product_EnsureCorrectCatergoryAttribute : ActionFilterAttribute
    {
        private readonly string[] _validCategories = new[] { "Shoes", "Hats", "Shirts", "Pants" };

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("model", out object? value) && value is Product product)
            {
                if (!_validCategories.Contains(product.Category))
                {
                    context.Result = new BadRequestObjectResult($"Invalid category. Allowed values are: {string.Join(", ", _validCategories)}");
                }
            }

            base.OnActionExecuting(context);
        }
    }
