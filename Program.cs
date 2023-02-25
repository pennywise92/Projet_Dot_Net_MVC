using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AchatEnLigne.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AchatEnLigne.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AchatEnLigneContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AchatEnLigneContext") ?? throw new InvalidOperationException("Connection string 'AchatEnLigneContext' not found.")));


// Add services to the container.
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Logins";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;


});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Normal"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
