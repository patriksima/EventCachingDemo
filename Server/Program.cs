using System.Reflection;
using EventCachingDemo.Server;
using EventCachingDemo.Server.Helpers;
using EventCachingDemo.Server.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add Mediatr for CQRS pattern
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add sql database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");

builder.Services.AddDbContext<MyContext>((_, optionsBuilder) =>
{
    optionsBuilder.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    // converts QueryContainer to Query and pass further
    options.ModelBinderProviders.Insert(0, new MessageBinderProvider());
});

// Build app
var app = builder.Build();

// apply migrations
app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();