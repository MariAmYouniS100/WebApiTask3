using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public  Department Department { get; set;}

    }
}
