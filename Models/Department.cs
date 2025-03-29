using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Employee> Employees = new List<Employee>();
    }
}
