using Microsoft.EntityFrameworkCore;
using Cine.Data;
using Cine.Data.Repos;
using Cine.Data.Context;
using Cine.Data.Inyecciones;


var builder = WebApplication.CreateBuilder(args);

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
