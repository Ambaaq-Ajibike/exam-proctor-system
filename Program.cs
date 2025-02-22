using exam_proctor_system.ApplicationContext;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Implementations;
using exam_proctor_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var connectionString = builder.Configuration.GetConnectionString("ProctorDB");

if (string.IsNullOrEmpty(connectionString))
{
	throw new InvalidOperationException("Database connection string is missing.");
}

Console.WriteLine($"Using Connection String: {connectionString}"); // Debugging output

builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
