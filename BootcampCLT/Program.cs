using BootcampCLT.Application.Query;
using BootcampCLT.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, services, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());


var envDirecta = Environment.GetEnvironmentVariable("ApplicationName");


var titleFromEnv = !string.IsNullOrEmpty(envDirecta)
                   ? envDirecta
                   : (builder.Configuration["ApplicationName"] ?? "API Default Name");

builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductosDb")));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetProductoByIdHandler).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuramos Swagger usando la variable que ya tiene la prioridad correcta
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = titleFromEnv,
        Version = "v1"
    });
});

// --- 3. CONSTRUIR LA APP ---
var app = builder.Build();

// --- 4. LOGS DE VERIFICACIÓN (Para ver en kubectl logs) ---
Console.WriteLine($"--- KUBERNETES ENV DIRECTA: {envDirecta} ---");
Console.WriteLine($"DEBUG: Application Name final: {titleFromEnv}");

// --- 5. CONFIGURAR PIPELINE (Middleware) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        // Esto cambia el título que aparece en la pestaña del navegador
        c.DocumentTitle = titleFromEnv;
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();
app.MapControllers();

app.Run();