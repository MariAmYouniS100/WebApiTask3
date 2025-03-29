using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Context;
using WebApplication1.Dto;
using WebApplication1.GenerateToken;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> userManager;
        private readonly ApplicationDbContext dbContext ;
        public AccountController(UserManager<AppUser> userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAccountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin");
              return Ok(new { message = "User registered successfully" });
            }
      

            return BadRequest(result.Errors);
        }


        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterAccountDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Student");
                return Ok(new { message = "User registered successfully" });
            }


            return BadRequest(result.Errors);
        }


            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto model)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                    return Unauthorized(new { message = "Invalid Inputs" });
            var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
                return Unauthorized(new { message = "Invalid Inputs" });

            var roles = await userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "User"; 
           
                var token = TokenService.GenerateToken(user.Id, user.UserName, role);

                return Ok(new { token });
            }


        [Authorize(Roles ="Admin")] 
        [HttpGet("claims-adminonly")]
        public IActionResult GetAdminClaims()
        {
            var userClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(userClaims);
        }

        [Authorize] 
        [HttpGet("claims-student")]
        public IActionResult GetStudentClaims()
        {
            var userClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(userClaims);
        }

    }
    }



