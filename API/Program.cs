using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext
builder.Services.AddDbContext<AuthorizationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Register repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ApplicationRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<UserRoleRepository>();

// Generate a random secret key (for development purposes)
builder.Services.AddScoped<AuthorizationService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var secretKey = config.GetValue<string>("AppSettings:SecretKey");

    // If secretKey is not defined in appsettings.json, generate a random one
    if (string.IsNullOrEmpty(secretKey))
    {
        secretKey = GenerateRandomSecretKey(32); // 32 bytes = 256 bits
    }

    return new AuthorizationService(
        provider.GetRequiredService<RoleRepository>(),
        provider.GetRequiredService<UserRoleRepository>(),
        provider.GetRequiredService<UserRepository>(),
        provider.GetRequiredService<ApplicationRepository>(),
        secretKey
    );
});

// Generate a random secret key
string GenerateRandomSecretKey(int length)
{
    using (var rng = new RNGCryptoServiceProvider())
    {
        byte[] byteArray = new byte[length];
        rng.GetBytes(byteArray);
        return Convert.ToBase64String(byteArray);
    }
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
