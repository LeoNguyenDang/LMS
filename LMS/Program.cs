using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Không tìm thấy connection string 'DefaultConnection'.");

builder.Services.AddDbContext<LmsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<LmsUser, IdentityRole>()
    .AddEntityFrameworkStores<LmsDbContext>()
    .AddDefaultTokenProviders();



builder.Services.AddControllersWithViews(); // Add services to the container.

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    var userManager = scope.ServiceProvider
        .GetRequiredService<UserManager<LmsUser>>();

    await LmsIdentitySeeder.SeedAsync(
        roleManager,
        userManager);
}



if (!app.Environment.IsDevelopment()) // Configure the HTTP request pipeline.
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
