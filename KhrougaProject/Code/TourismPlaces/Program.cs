using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TourismPlaces.Data;
using TourismPlaces.Data.Models;
using TourismPlaces.IRepository;
using TourismPlaces.Mapper;
using TourismPlaces.Repository;
using TourismPlaces.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//**************************************************************************************************
//Register AutoMapper in Services
builder.Services.AddAutoMapper(typeof(MappingConfig));

//**************************************************************************************************

//Register Application Context in services
builder.Services.AddDbContext<ApplicationDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
    opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//**************************************************************************************************

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

//**************************************************************************************************


builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddScoped<IPlaceRepo, PlaceRepo>();
builder.Services.AddScoped<IPlaceApproveRepo, PlaceApproveRepo>();
builder.Services.AddMvc(
    options =>
    {
        // This pushes users to login if not authenticated
        options.Filters.Add(new AuthorizeFilter());
    }
    ).AddNToastNotifyToastr(new ToastrOptions()
{
    CloseButton = true,
    PositionClass = ToastPositions.TopRight,
    PreventDuplicates = true,
    ProgressBar = true,
  
});
builder.Services.AddScoped<PhotosFileViewModel>();
builder.Services.AddScoped<IDashboardBoardRepo, DashboardBoardRepo>();





builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequireNonAlphanumeric = true;
    opts.Password.RequiredUniqueChars = 1;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireDigit = true;
    opts.User.RequireUniqueEmail = true;
    
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Account/Index";
    config.AccessDeniedPath = "/Account/Index/{id?}";
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
    pattern: "{controller=Account}/{action=HomePage}/{id?}");

app.Run();
