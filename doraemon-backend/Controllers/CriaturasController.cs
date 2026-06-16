using Microsoft.AspNetCore.Mvc;
using doraemon_backend.Models;
using doraemon_backend.Negocio;

namespace doraemon_backend.Controllers;

// =========================================================
// CONTROLADOR: CriaturasController
//
// Identico al original — el controlador no cambia nada.
// Sigue siendo el "portero": recibe HTTP, llama al servicio,
// devuelve la respuesta. La BD es transparente para el.
// =========================================================

[ApiController]
[Route("api/criaturas")]
public class CriaturasController(CriaturaService servicio) : ControllerBase
{
    // GET /api/criaturas
    [HttpGet]
    public IActionResult ObtenerTodas()
    {
        var criaturas = servicio.ObtenerTodas();
        return Ok(criaturas);
    }

    // GET /api/criaturas/{id}
    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var criatura = servicio.ObtenerPorId(id);
        if (criatura is null)
            return NotFound(new { mensaje = $"No existe criatura con ID {id}." });
        return Ok(criatura);
    }

    // POST /api/criaturas
    [HttpPost]
    public IActionResult Crear([FromBody] CrearCriaturaRequest request)
    {
        var (ok, error, criatura) = servicio.Crear(request);
        if (!ok)
            return BadRequest(new { mensaje = error });
        return CreatedAtAction(nameof(ObtenerPorId), new { id = criatura!.Id }, criatura);
    }

    // DELETE /api/criaturas/{id}
    [HttpDelete("{id}")]
    public IActionResult Eliminar(int id)
    {
        var (ok, error) = servicio.Eliminar(id);
        if (!ok)
            return NotFound(new { mensaje = error });
        return NoContent();
    }

    // POST /api/criaturas/atacar
    [HttpPost("atacar")]
    public IActionResult Atacar([FromBody] AtaqueRequest request)
    {
        var (ok, error, resultado) = servicio.SimularAtaque(request);
        if (!ok)
            return BadRequest(new { mensaje = error });
        return Ok(resultado);
    }
}
