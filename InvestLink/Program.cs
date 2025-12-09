using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Mapper;
using InvestLink_BLL.Repository;
using InvestLink_DAL.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//ConnectionStrings
var connectionString = builder.Configuration.GetConnectionString("InvestLink");
builder.Services.AddDbContext<MyContext>(options =>
options.UseSqlServer(connectionString));



builder.Services.AddAutoMapper(typeof(DomainProfile));
builder.Services.AddScoped<IProject, ProjectRepo>();

builder.Services.AddScoped<INationality, NationalityRepo>();
builder.Services.AddScoped<IEmployee, EmployeeRepo>();
builder.Services.AddScoped<IInvestor, InvestorRepo>();
builder.Services.AddScoped<IProjectInvestor, ProjectInvestorRepo>();
builder.Services.AddScoped<ILicense, LicenseRepo>();
builder.Services.AddScoped<ICoordinatorReport, CoordinatorReportRepo>();
builder.Services.AddScoped<IAdvertisement, AdvertisementRepo>();
builder.Services.AddScoped<IProjectCoordinator, ProjectCoordinatorRepo>();

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
