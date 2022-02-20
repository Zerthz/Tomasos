using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T�mas�s.Models;
using T�mas�s.ModelsIdentity;
using T�mas�s.Repository;
using T�mas�s.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDishRepo, DishRepo>();
builder.Services.AddTransient<IDishService, DishService>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
builder.Services.AddTransient<IAdminService, AdminService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(40));

builder.Services.AddMvc(options => options.EnableEndpointRouting=false);

var connString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<TomasosDbContext>(options => options.UseSqlServer(connString));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

var app = builder.Build();

app.UseAuthentication();
app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}");
});





app.UseStaticFiles();

app.Run();
