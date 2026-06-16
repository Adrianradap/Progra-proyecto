using doraemon_backend.Datos;
using doraemon_backend.Negocio;
using Microsoft.EntityFrameworkCore;

// =========================================================
// PROGRAM.CS — Punto de entrada de la aplicacion
//
// Cambios respecto al original:
//   1. Se registra AppDbContext con SQLite (antes no existia)
//   2. CriaturaService ahora es Scoped (no Singleton) porque
//      depende de DbContext que tambien es Scoped
//   3. Se aplican migraciones automaticamente al arrancar
//   4. CORS apunta a Angular en localhost:4200
// =========================================================

var builder = WebApplication.CreateBuilder(args);

// --- Base de datos SQLite via Entity Framework Core ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Servicios de negocio ---
builder.Services.AddControllers();
builder.Services.AddScoped<CriaturaService>(); // Scoped porque usa DbContext

// --- CORS para Angular frontend ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// --- Aplicar migraciones y seed automaticamente al arrancar ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// --- Pipeline HTTP ---
app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

app.Run();
