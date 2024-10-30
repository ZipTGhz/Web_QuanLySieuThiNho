using Microsoft.EntityFrameworkCore;
using Web_QuanLySieuThiNho.Models;
using Web_QuanLySieuThiNho.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//??c connection string t? appsetting.json
var connectionString = builder.Configuration.GetConnectionString("QlsieuThiNhoContext");
builder.Services.AddDbContext<QlsieuThiNhoContext>(x=>x.UseSqlServer(connectionString));

builder.Services.AddScoped<ILoaiHangRepo, LoaiHangRepo>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();  // Thêm IHttpContextAccessor

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
