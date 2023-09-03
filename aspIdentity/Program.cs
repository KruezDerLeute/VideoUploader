using aspIdentity.Data;
using aspIdentity.Data.Model;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConString"))); //Establishing a connection to Database

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager<SignInManager<AppUser>>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 150 * 1024 * 1024;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 150 * 1024 * 1024;   
});

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login";  //if an unanoymous user tries to access to authorized page, 
                                  //he or she will be redirected to Login Page.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
