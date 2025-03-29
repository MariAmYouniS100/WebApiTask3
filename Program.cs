
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebApplication1.Context;
using WebApplication1.GenerateToken;
using WebApplication1.Middlware;
using WebApplication1.Models;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

            builder.Services.AddCors(op =>
            {
                op.AddPolicy("allowanyuser",
                    policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7155",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789012345612345678@#$%^SDFDEKJSKDJSKJOLPNMDMDMSHHTRSZPOQA")),
             RoleClaimType = ClaimTypes.Role
        };
    });

            var app = builder.Build();

            app.UseMiddleware<HandleExecption>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseCors("allowanyuser");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
