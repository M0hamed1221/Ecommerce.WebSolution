using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attribute
{
   public class RedisCacheAtrribute(int durationInSec = 1800) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
            //1)Create Cache Key
            var key = CreateCacheKey(context.HttpContext.Request);
            //2)Search With That Key In Redis
            var cacheValue = await cacheService.GetAsync(key);

            //2.1)Found     => Return Object 
            //2.2)Not Found => Execute Endpoint   / Take Result And Store In Redis 
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult res)
                await cacheService.SetAsync(key, res.Value!, TimeSpan.FromSeconds(durationInSec));


        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(request.Path + "?");

            foreach (var item in request.Query.OrderBy(q => q.Key))
                builder.Append($"{item.Key}={item.Value}&");

            return builder.ToString().Trim('&');

        }

    }
}
