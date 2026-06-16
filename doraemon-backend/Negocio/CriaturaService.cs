using doraemon_backend.Datos;
using doraemon_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace doraemon_backend.Negocio;

// =========================================================
// NEGOCIO: CriaturaService
//
// Misma logica que antes — NINGUN cambio de reglas de negocio.
// El unico cambio es que ahora en lugar de usar una lista
// en memoria (_criaturas), se usa AppDbContext para leer
// y escribir en la base de datos SQLite.
//
// AppDbContext se inyecta igual que antes era el servicio.
// =========================================================

public class CriaturaService
{
    private readonly AppDbContext _db;

    public CriaturaService(AppDbContext db)
    {
        _db = db;
    }

    // ---------------------------------------------------------
    // OBTENER TODAS LAS CRIATURAS
    // ---------------------------------------------------------
    public List<Criatura> ObtenerTodas()
    {
        return _db.Criaturas.ToList();
    }

    // ---------------------------------------------------------
    // OBTENER UNA CRIATURA POR ID
    // ---------------------------------------------------------
    public Criatura? ObtenerPorId(int id)
    {
        return _db.Criaturas.FirstOrDefault(c => c.Id == id);
    }

    // ---------------------------------------------------------
    // CREAR NUEVA CRIATURA
    // Mismas validaciones de negocio — ahora persiste en BD
    // ---------------------------------------------------------
    public (bool Ok, string Error, Criatura? Criatura) Crear(CrearCriaturaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
            return (false, "El nombre es obligatorio.", null);

        var tiposValidos = new[] { "Fuego", "Agua", "Planta", "Roca", "Electrico" };
        if (!tiposValidos.Contains(request.Tipo))
            return (false, $"Tipo invalido. Usa: {string.Join(", ", tiposValidos)}", null);

        if (request.Hp <= 0 || request.Atk <= 0 || request.Def <= 0)
            return (false, "HP, ATK y DEF deben ser mayores a 0.", null);

        if (_db.Criaturas.Any(c => c.Nombre.ToLower() == request.Nombre.Trim().ToLower()))
            return (false, "Ya existe una criatura con ese nombre.", null);

        var nueva = new Criatura
        {
            Nombre = request.Nombre.Trim(),
            Tipo   = request.Tipo,
            Hp     = request.Hp,
            Atk    = request.Atk,
            Def    = request.Def
        };

        _db.Criaturas.Add(nueva);
        _db.SaveChanges(); // Persiste en SQLite
        return (true, string.Empty, nueva);
    }

    // ---------------------------------------------------------
    // ELIMINAR CRIATURA
    // ---------------------------------------------------------
    public (bool Ok, string Error) Eliminar(int id)
    {
        var criatura = _db.Criaturas.FirstOrDefault(c => c.Id == id);
        if (criatura is null)
            return (false, "Criatura no encontrada.");

        _db.Criaturas.Remove(criatura);
        _db.SaveChanges();
        return (true, string.Empty);
    }

    // ---------------------------------------------------------
    // SIMULAR ATAQUE ENTRE DOS CRIATURAS
    // Formula identica: (ATK - DEF) * multiplicador + 10
    // Ahora el HP actualizado se guarda en la BD
    // ---------------------------------------------------------
    public (bool Ok, string Error, AtaqueResultado? Resultado) SimularAtaque(AtaqueRequest request)
    {
        var atacante = _db.Criaturas.FirstOrDefault(c => c.Id == request.AtacanteId);
        var objetivo = _db.Criaturas.FirstOrDefault(c => c.Id == request.ObjetivoId);

        if (atacante is null) return (false, "Atacante no encontrado.", null);
        if (objetivo is null) return (false, "Objetivo no encontrado.", null);
        if (atacante.Id == objetivo.Id) return (false, "Una criatura no puede atacarse a si misma.", null);

        double multiplicador = ObtenerVentajaTipo(atacante.Tipo, objetivo.Tipo);
        int danio = Math.Max(1, (int)((atacante.Atk - objetivo.Def) * multiplicador) + 10);
        int hpRestante = Math.Max(0, objetivo.Hp - danio);

        // Actualizar HP en la base de datos
        objetivo.Hp = hpRestante;
        _db.SaveChanges();

        var resultado = new AtaqueResultado
        {
            Atacante   = $"{atacante.Nombre} ({atacante.Tipo})",
            Objetivo   = $"{objetivo.Nombre} ({objetivo.Tipo})",
            Danio      = danio,
            HpRestante = hpRestante,
            Derrotado  = hpRestante <= 0,
            Mensaje    = multiplicador > 1.0
                ? $"¡Es muy efectivo! {atacante.Nombre} causa {danio} de daño a {objetivo.Nombre}."
                : multiplicador < 1.0
                    ? $"No es muy efectivo. {atacante.Nombre} causa {danio} de daño a {objetivo.Nombre}."
                    : $"{atacante.Nombre} causa {danio} de daño a {objetivo.Nombre}."
        };

        return (true, string.Empty, resultado);
    }

    // ---------------------------------------------------------
    // TABLA DE VENTAJAS DE TIPO — logica intacta
    // ---------------------------------------------------------
    private static double ObtenerVentajaTipo(string tipoAtaque, string tipoDefensa)
    {
        return (tipoAtaque, tipoDefensa) switch
        {
            ("Fuego",     "Planta")    => 1.5,
            ("Agua",      "Fuego")     => 1.5,
            ("Planta",    "Roca")      => 1.5,
            ("Roca",      "Electrico") => 1.5,
            ("Electrico", "Agua")      => 1.5,
            ("Fuego",     "Agua")      => 0.5,
            ("Planta",    "Fuego")     => 0.5,
            ("Roca",      "Planta")    => 0.5,
            ("Electrico", "Roca")      => 0.5,
            ("Agua",      "Electrico") => 0.5,
            _ => 1.0
        };
    }
}
