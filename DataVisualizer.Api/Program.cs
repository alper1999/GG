using DataVisualizer.Api.Controllers;
using DataVisualizer.Api.Services;

public class Program  // Ensure Program class is public
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost3000", policy =>
            {
                policy.WithOrigins("http://localhost:3000") // Gjør at localhost:3000 kan aksessere API-et
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // Register CsvService
        builder.Services.AddScoped<CsvService>();

        var app = builder.Build();

        // Use CORS middleware
        app.UseCors("AllowLocalhost3000");

        // Enabling HTTPS redirection
        app.UseHttpsRedirection();

        // Enabling Authorization middleware
        app.UseAuthorization();

        // Map controllers for the API endpoints
        app.MapControllers();

        // Run the application
        app.Run();
    }
}
