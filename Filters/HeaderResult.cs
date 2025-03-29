using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class HeaderResult : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("APPName-Custom-Header", "MyApp");

        }
    }
}
