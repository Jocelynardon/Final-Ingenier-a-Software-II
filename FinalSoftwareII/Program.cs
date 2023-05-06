using FinalSoftwareII.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(s =>

{

    s.IdleTimeout = TimeSpan.FromMinutes(20);

    s.Cookie.Name = ".aWWW.Session"; /*You can change this variable*/

    s.Cookie.Expiration = TimeSpan.FromMinutes(20);

});



builder.Services.AddCors(options =>

{

    options.AddPolicy("AllOrigins",

        builder =>

        {

            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();

        });

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

               .AddCookie(options =>

               {

                   options.Cookie.SameSite = SameSiteMode.None;

                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                   options.LoginPath = "/Home/Index"; /*this option indicates where is the login page*/

               });

var conexion = builder.Configuration.GetConnectionString("ConnectionDB");
builder.Services.AddDbContext<FinalSoftwareContext>(Options => Options.UseMySQL(conexion));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();