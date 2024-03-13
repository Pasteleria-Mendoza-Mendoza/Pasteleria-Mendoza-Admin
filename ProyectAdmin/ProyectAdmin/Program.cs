using Microsoft.AspNetCore.Authentication.Cookies;
using ProyectAdmin.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddProyectDEpendecies(builder.Configuration);
builder.Services.AddHttpContextAccessor();

//Agregar Authenticacion al Sistema.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((o) =>
{
    // Aca se redireccionara en caso que no posea permiso. Ira a la vista de Login.
    o.LoginPath = new PathString("/Security/Login");
    //tiempo de duracion de las cookies, en este caso se han puesto 8 horas.
    o.ExpireTimeSpan = TimeSpan.FromHours(10);
    o.AccessDeniedPath = new PathString("/Security/AccessDenied");
    o.SlidingExpiration = true;
});

builder.Services.AddProyectDEpendecies(builder.Configuration);

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
