using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static MovimientoDtos;

[ApiController]
[Route("movimientos")]
public class MovimientosController : ControllerBase
{
    private readonly MovimientoService _svc;
    private readonly IUnitOfWork _uow;
    public MovimientosController(MovimientoService svc, IUnitOfWork uow)
    {
        _svc = svc; _uow = uow;
    }

    [HttpPost]
    public async Task<ActionResult<MovimientoDto>> Create([FromBody] CrearMovimientoRequest req)
    {
        var (ok, error, mov) = await _svc.RegistrarMovimientoAsync(req.CuentaId, req.TipoMovimiento, req.Valor);
        if (!ok) return BadRequest(error);

        var dto = new MovimientoDto(mov!.MovimientoId, mov.Fecha, mov.TipoMovimiento, mov.Valor, mov.Saldo, mov.CuentaId);
        return CreatedAtAction(nameof(GetById), new { id = mov.MovimientoId }, dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MovimientoDto>> GetById(Guid id)
    {
        var m = await _uow.Movimientos.GetByIdAsync(id);
        return m is null ? NotFound() : Ok(new MovimientoDto(m.MovimientoId, m.Fecha, m.TipoMovimiento, m.Valor, m.Saldo, m.CuentaId));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimientoDto>>> List([FromQuery] Guid? cuentaId, [FromQuery] DateTime? desde, [FromQuery] DateTime? hasta)
    {
        var list = await _uow.Movimientos.ListarAsync(cuentaId, desde, hasta);
        return Ok(list.Select(m => new MovimientoDto(m.MovimientoId, m.Fecha, m.TipoMovimiento, m.Valor, m.Saldo, m.CuentaId)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var m = await _uow.Movimientos.GetByIdAsync(id);
        if (m is null) return NotFound();
        _uow.Movimientos.Remove(m);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}