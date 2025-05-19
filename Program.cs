using CSnakes.Runtime; // Added for CSnakes
// using CSnakes.Runtime.Extensions.DependencyInjection; // Removed
using System.IO; // Added for Path.Combine

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure CSnakes
var home = Path.Join(Environment.CurrentDirectory, "python");
var venvPath = Path.Combine(home, "venv");

builder.Services.WithPython()
    .WithHome(home) // Path to your Python modules
    .FromRedistributable()
    .WithVirtualEnvironment(venvPath) // Assuming your venv is directly inside the 'python' folder
    .WithPipInstaller(); // If you have a requirements.txt

// Register your SentimentAnalysis class for Dependency Injection
builder.Services.AddScoped<SentimentWebApp.PythonIntegration.SentimentAnalysis>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Static files (like CSS, JS from wwwroot, and potentially Python files if CopyToOutputDirectory is used)
app.UseStaticFiles(); // Ensure UseStaticFiles is present and correctly configured

app.UseRouting();

app.UseAuthorization();

// This was app.MapStaticAssets(); - CSnakes might have specific guidance if it replaces/enhances this.
// For now, standard static asset mapping is usually MapStaticAssets or UseStaticFiles.
// If CSnakes generates assets to be served, ensure they are covered.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    // .WithStaticAssets(); // This was chained here, ensure its compatibility/necessity with UseStaticFiles and CSnakes


app.Run();
