using AuthService.Data;
using AuthService.Repository;
using AuthService.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using AuthService.Utilities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;

public class Program{

    public static void Main(string[] args){
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OticaContext>(options =>
{
    options.UseMySql(connectionString,MySqlServerVersion.AutoDetect(connectionString));
});

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddDataProtection()
    .PersistKeysToDbContext<OticaContext>();

Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
Console.WriteLine($"Jwt Key: {Environment.GetEnvironmentVariable("Jwt_Key")}");
Console.WriteLine($"Jwt Issuer: {Environment.GetEnvironmentVariable("Jwt_Issuer")}");
Console.WriteLine($"Jwt Audience: {Environment.GetEnvironmentVariable("Jwt_Audience")}");
Console.WriteLine($"Connection String: {Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")}");

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("_myAllowSpecificOrigins");
app.UseRouting();

app.MapControllers();

app.Run();


}
}