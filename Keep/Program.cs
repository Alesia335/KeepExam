using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Keep.Data;
using Keep.Areas.Identity.Data;
using Keep.Repositories;
using Keep.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("KeepContextConnection") ?? throw new InvalidOperationException("Connection string 'KeepContextConnection' not found.");


builder.Services.AddDbContext<KeepContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<KeepUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<KeepContext>();;

builder.Services.AddTransient<NotesRepository>();
builder.Services.AddTransient<UserService>();

// Add services to the container.
builder.Services.AddRazorPages();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
