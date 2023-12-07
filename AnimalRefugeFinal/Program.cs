using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AnimalRefugeFinal.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//get Connection string from ToDoContext
builder.Services.AddDbContext<AnimalRefugeFinal.Models.PetContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PetContext")));

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 10;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<PetContext>()
    .AddDefaultTokenProviders();

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

// routes
app.UseEndpoints(endpoints =>
{
    // default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
