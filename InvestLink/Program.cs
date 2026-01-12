using InvestLink_BLL.Interfaces;
using InvestLink_BLL.Mapper;
using InvestLink_BLL.Repository;
using InvestLink_DAL.DataBase;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//ConnectionStrings

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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

//----------------------------------------
builder.Services.AddMvc().AddNToastNotifyToastr();

//----------------------------------------

//لو الايميل و بااسس صح حيفتح سيشن مع المتصفح

//------------------Authentication----------------
builder.Services
.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.AccessDeniedPath = new PathString("/Account/Login");
});
//----------------------------------------------

//// Generate Token
builder.Services.AddIdentityCore<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = false)
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MyContext>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

//-----------------------------------------------




// Password Configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Default Password settings.
    //Unique in IdentityUser
    options.User.RequireUniqueEmail = true;
    //-----------------------------------------------
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<MyContext>();
//-----------------------------------------------

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
