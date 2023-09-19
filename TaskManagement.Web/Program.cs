using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Persistence;
using TaskManagment.Domain;
using System.Reflection;
using TaskManagement.Web.Models;
using FluentValidation;
using TaskManagement.Web.Validation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddRazorPages();
builder.Services.AddScoped<IValidator<CreateTaskModel>, CreateTaskModelValidator>();
builder.Services.AddScoped<IValidator<EditTaskModel>, EditTaskModelValidator>();

builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddDbContext<TaskManagementDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<TaskManagementDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<UserEntity, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<TaskManagementDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";
    });
var app = builder.Build();
SeedData.Seed(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
