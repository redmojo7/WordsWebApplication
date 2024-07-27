using System.Net;
using WordsWebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<INumberToWordsConvertService, NumberToWordsConvertService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
//builder.Logging.AddDebug();
// builder.Logging.AddEventSourceLogger();

var app = builder.Build();

// Get the logger and hosting environment
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

logger.LogInformation("Application is starting...");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Convert/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Convert}/{action=Index}/{id?}");

logger.LogInformation($"Now listening on http://localhost:5289/");


logger.LogInformation("Application is configured and ready to run.");

app.Run();
