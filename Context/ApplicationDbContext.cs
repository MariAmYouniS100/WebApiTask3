using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Context
{
    public class ApplicationDbContext :  IdentityDbContext<AppUser>
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "A1B2C3D4-E5F6-7890-1234-56789ABCDEF0",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id ="B1C2D3E4-F5G6-7890-2345-67890ABCDEF1",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    

}
}
