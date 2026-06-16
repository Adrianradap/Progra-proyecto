namespace doraemon_backend.Models;

// =========================================================
// MODELO: Criatura
// Representa los datos de cada criatura del juego.
// =========================================================

public class Criatura
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;  // Fuego, Agua, Planta, Roca, Electrico
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
}

// =========================================================
// MODELO: Solicitud para crear una criatura (Request DTO)
// =========================================================

public class CrearCriaturaRequest
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
}

// =========================================================
// MODELO: Solicitud de ataque entre dos criaturas
// =========================================================

public class AtaqueRequest
{
    public int AtacanteId { get; set; }
    public int ObjetivoId { get; set; }
}

// =========================================================
// MODELO: Resultado del ataque (Response DTO)
// =========================================================

public class AtaqueResultado
{
    public string Mensaje { get; set; } = string.Empty;
    public string Atacante { get; set; } = string.Empty;
    public string Objetivo { get; set; } = string.Empty;
    public int Danio { get; set; }
    public int HpRestante { get; set; }
    public bool Derrotado { get; set; }
}
