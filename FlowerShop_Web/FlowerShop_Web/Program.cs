using FloweShop_Web.Models.Flower_Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.Emit;
using FlowerShop_Web.Models.Flower_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.DesignPattern;
using FlowerShop_Web.Models.Pattern;
using FlowerShop_Web.Chat;
using FlowerShop_Web.Models.Flower_Reponsitory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("FlowerShop_Web")));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<Import_ExportService>();
builder.Services.AddScoped<MementoPattern>();
builder.Services.AddScoped<FlowerShop>();
builder.Services.AddScoped<StatusOrderCommand>();
builder.Services.AddScoped<DeliveredOrderCommand>();
builder.Services.AddScoped<CompletedOrderCommand>();

builder.Services.AddScoped<Order>();
builder.Services.AddSignalR();




builder.Services.AddMvc(); // Đăng ký MVC services



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});
// Identity
builder.Services.
    AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).
    AddEntityFrameworkStores<ApplicationDbContext>().
    AddDefaultUI().
    AddDefaultTokenProviders();


// Add PayPal-related services
builder.Services.AddScoped<IPaymentStrategy, OnlinePaymentStrategy>();
builder.Services.AddScoped<IPaymentStrategy, CODPaymentStrategy>();
builder.Services.AddScoped<OnlinePaymentStrategy>();
builder.Services.AddScoped<CODPaymentStrategy>();

builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddRazorPages();

builder.Services.AddMemoryCache();

builder.Services.AddSignalR();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<AccessCounterMiddleware>();


#pragma warning restore ASP0014 // Suggest using top level route registrations

app.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.UseRouting();
// Thêm middleware xác thực
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapRazorPages();

app.MapHub<ChatHub>("/chathub");

app.Run();