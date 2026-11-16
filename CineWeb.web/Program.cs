using Cine.Data;
using Cine.Data.Context;
using Cine.Data.Inyecciones;
using Cine.Data.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme =
   CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
 .AddCookie()
 .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
 {
     options.ClientId =
    builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
     options.ClientSecret =
    builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
});

builder.Services.AddControllersWithViews();

builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("CineDb"));


var app = builder.Build();



//-----*-----


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
