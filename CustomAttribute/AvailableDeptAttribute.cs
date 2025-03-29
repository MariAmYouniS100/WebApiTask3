using System.ComponentModel.DataAnnotations;
using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.CustomAttribute
{
    public class AvailableDeptAttribute : ValidationAttribute 
    {

       protected override ValidationResult? IsValid(object? value , ValidationContext validationContext)
        {
            var dBContext = validationContext.GetService<ApplicationDbContext>();
            int? deptid;
            Department department=null;

           department = dBContext.Departments.Find((int)value);

            if (department == null)
                return new ValidationResult(ErrorMessage ?? "The selected department does not exist.");
            return ValidationResult.Success;
        }

    }
}
