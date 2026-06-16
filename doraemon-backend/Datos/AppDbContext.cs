using Microsoft.EntityFrameworkCore;
using doraemon_backend.Models;

namespace doraemon_backend.Datos;

// =========================================================
// DATOS: AppDbContext
//
// Esta clase es el "puente" entre C# y la base de datos.
// Entity Framework Core usa esta clase para:
//   - Crear y actualizar la estructura de la BD (tablas)
//   - Traducir operaciones C# a SQL automaticamente
//
// DbSet<Criatura> le dice a EF que existe una tabla "Criaturas"
// =========================================================

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Tabla: Criaturas
    public DbSet<Criatura> Criaturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos semilla: las 5 criaturas iniciales del juego
        // (las mismas que antes estaban en la lista en memoria)
        modelBuilder.Entity<Criatura>().HasData(
            new Criatura { Id = 1, Nombre = "Brasa",   Tipo = "Fuego",     Hp = 118, Atk = 64, Def = 50 },
            new Criatura { Id = 2, Nombre = "Aqualin", Tipo = "Agua",      Hp = 124, Atk = 58, Def = 65 },
            new Criatura { Id = 3, Nombre = "Verdel",  Tipo = "Planta",    Hp = 126, Atk = 58, Def = 58 },
            new Criatura { Id = 4, Nombre = "Griom",   Tipo = "Roca",      Hp = 136, Atk = 64, Def = 70 },
            new Criatura { Id = 5, Nombre = "Voltix",  Tipo = "Electrico", Hp = 110, Atk = 66, Def = 44 }
        );
    }
}
