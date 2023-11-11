using System.Reflection;
using API.Extensions;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddSwaggerGen(
    c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); // Use First() as a workaround
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversidadA", Version = "v1" });
    c.SwaggerDoc("v1.1", new OpenApiInfo { Title = "UniversidadA v1.1", Version = "v1.1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
}
);
builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

builder.Services.AddDbContext<UniversidadAContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
