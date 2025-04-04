using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MealAPI", Version = "v1" });
});
builder.Services.AddDbContext<MealDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information));


// Add CORS policy - DEVELOPMENT ONLY (allows any origin)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});


var app = builder.Build();

// Use CORS middleware - must be after builder.Build() and before other middleware
app.UseCors("AllowAll");


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MealAPI v1"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MealDbContext>();

    try
    {
        dbContext.Database.CanConnect();
        Console.WriteLine("✅ Connection to the database was successful!");

        // Add a test log to Swagger UI
        app.MapGet("/test-connection", () => "✅ Database connection is successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error connecting to the database: {ex.Message}");

        // Log error in Swagger
        app.MapGet("/test-connection", () => $"❌ Database error: {ex.Message}");
    }
}

app.Run();
