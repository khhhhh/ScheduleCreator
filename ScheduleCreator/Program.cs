using Microsoft.EntityFrameworkCore;
using SchedulePlan.DataAccess;
using Microsoft.AspNetCore.Identity;
using SchedulePlan.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'AppDBContextConnection' not found.");

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDBContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();;
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});
app.Run();
