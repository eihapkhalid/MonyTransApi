using Bl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MoneyTransfer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<MoneyTransferContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddScoped<IBusinessLayer<TbUser>, ClsUsers>();
            builder.Services.AddScoped<IBusinessLayer<TbFinancialTransaction>, ClsFinancialTransaction>();
            builder.Services.AddScoped<IBusinessLayer<VwTransWacountDetail>, ClsVwTransWacountDetails>();

            // Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            

            #region UseEndpoints routing
            app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Users}/{action=List}");
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Trans}/{action=Index}/{id?}");
    }); 
            #endregion

            app.Run();
        }
    }
}