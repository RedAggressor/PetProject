using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Attributes
{
    public class ValidateRequestBodyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            if(request.Body == null)
            {
                context.Result = new OkObjectResult(new BaseResponse()
                {
                    ErrorMessage = "Request body is null!"
                });

                return;
            }

            var body = context.ActionArguments.Values.FirstOrDefault();

            if (body == null)
            {
                context.Result = new OkObjectResult(new BaseResponse
                {
                    ErrorMessage = "Request body cannot be null"
                    
                });

                return;
            }

            foreach (var property in body.GetType().GetProperties())
            {
                var value = property.GetValue(body);
                
                if (value == null)
                {
                    context.Result = new OkObjectResult(new BaseResponse
                    {
                        ErrorMessage = $"Field {property.Name} cannot be null or empty"                       
                    });

                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
