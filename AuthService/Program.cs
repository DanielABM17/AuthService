using AuthService.Data;
using AuthService.Repository;
using AuthService.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using AuthService.Utilities;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("_myAllowSpecificOrigins");
app.UseRouting();

app.MapControllers();

app.Run();


}
}