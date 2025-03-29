using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Dto;

namespace WebApplication1.Filters
{
    public class SalaryCheck : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        // before excuation
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.TryGetValue("employeeAddDto", out var value) && value is EmployeeAddDto employee)
            {
                if ( employee.Salary < 10 || employee.Salary > 10000000)
                {
                    context.Result = new BadRequestObjectResult("Salary must be at least 10");
                }
            }
        }
    }
}
