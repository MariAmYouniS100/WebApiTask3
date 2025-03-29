using System.ComponentModel.DataAnnotations;
using WebApplication1.CustomAttribute;

namespace WebApplication1.Dto
{
    public class EmployeeUpdateDto //for read , update because it have id required
    {
        [Required]
        public int Id { get; set; } 
        [Required(ErrorMessage = "name is required")]
        [RegularExpression(@"^[A-Za-z]{3,}(?:\s[A-Za-z]{3,})?$", ErrorMessage = "name must be like ahmed")]
        // must start with 3 min charcahters , if add second name must be at least also 3
        [MaxLength(100, ErrorMessage = "name is ver big")]
        public string Name { get; set; }
        [Required(ErrorMessage = "salary is required")]
        [Range(100, 1000000, ErrorMessage = "salary is not accepted")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "department is required")]
        [AvailableDept(ErrorMessage = "department is not available")]
        public int DepartmentId { get; set; }

    }
}
