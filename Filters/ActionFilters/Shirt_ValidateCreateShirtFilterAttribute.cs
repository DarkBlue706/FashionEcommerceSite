
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShirtCompany.Models;




namespace ShirtCompany.Filters.ActionFilters
{
    public class Shirt_ValidateCreateShirtFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirt = context.ActionArguments["shirt"] as Shirt;
            

                if(shirt == null )
                {
                    context.ModelState.AddModelError("Shirt", "Shirt object is null.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                
                // var existingShirt = ShirtRepository.GetShirtByProperties(shirt?.Name, shirt?.Brand, shirt?.Description, shirt?.Color, shirt?.Size, shirt?.Gender, shirt?.CreatedDate);
                // if (existingShirt != null)
                // {
                //     context.ModelState.AddModelError("Shirt", "Shirt already exists.");
                //     var problemDetails = new ValidationProblemDetails(context.ModelState)
                //     {
                //         Status = StatusCodes.Status400BadRequest
                //     };
                //     context.Result = new BadRequestObjectResult(problemDetails);
                // }

                

            }

        }

    }
