using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Dto;
using WebApplication1.Filters;
using WebApplication1.Middlware;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [HeaderResult] // result filter
        public IActionResult Get()
        {
            List<EmployeeReadDto> employees = new List<EmployeeReadDto>();
            var employessfromdb = dbContext.Employees.Include(e=>e.Department).AsNoTracking().ToList();
            foreach(var emp in employessfromdb)
            {
                employees.Add(new EmployeeReadDto()
                {
                    Id = emp.Id,
                    Name=emp.Name,
                    Salary=emp.Salary,
                    Department=new DepartmentDto() { Id=emp.Department.Id, Name=emp.Department.Name}
                });
            }

            return Ok(employees);
        }


        [HttpGet("{id}")]
        public IActionResult GetbyId([FromRoute] int id)
        {
            EmployeeReadDto employee = new EmployeeReadDto();
            var employeefromdb = dbContext.Employees.Include(e => e.Department).FirstOrDefault(e=>e.Id==id);
            if (employeefromdb == null) throw new CustomExecption("employee not found");

            employee.Id = employeefromdb.Id;
            employee.Name = employeefromdb.Name;
            employee.Salary = employeefromdb.Salary;
            employee.Department = new DepartmentDto() { Id = employeefromdb.Department.Id, Name = employeefromdb.Department.Name };

            return Ok(employee);
        }


        [HttpPost]
        [SalaryCheck] // action filter
        public IActionResult Add([FromBody]EmployeeAddDto employeeAddDto)
        {

            dbContext.Employees.Add(new Employee()
            {
           Name=employeeAddDto.Name,
          Salary=employeeAddDto.Salary,
          DepartmentId=employeeAddDto.DepartmentId
            });

            dbContext.SaveChanges();

            return Ok(new {message="data add success" });
        }


        [HttpPut]
        public IActionResult Update([FromBody] EmployeeUpdateDto employeeUpdateDto)
        {

            var oldemployee = dbContext.Employees.Find(employeeUpdateDto.Id);
            if(oldemployee==null) throw new CustomExecption("employee not found");
            oldemployee.Salary = employeeUpdateDto.Salary;
            oldemployee.Name = employeeUpdateDto.Name;
            oldemployee.DepartmentId = employeeUpdateDto.DepartmentId;
            dbContext.SaveChanges();

            return Ok( new { message = "data updated success" });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {

            var oldemployee = dbContext.Employees.Find(id);
            if (oldemployee == null) throw new CustomExecption("employee not found");
            dbContext.Employees.Remove(oldemployee);
            dbContext.SaveChanges();

            return Ok(new { message = "data deleted success" });
        }


    }
}
