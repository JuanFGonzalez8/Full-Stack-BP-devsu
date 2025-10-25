using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static CuentaDtos;

[ApiController]
[Route("cuentas")]
public class CuentasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    public CuentasController(IUnitOfWork uow) => _uow = uow;

    [HttpPost]
    public async Task<ActionResult<CuentaDtos>> Create([FromBody] CrearCuentaRequest req)
    {
        var cliente = await _uow.Clientes.GetByIdAsync(req.ClienteId);
        if (cliente is null) return BadRequest("Cliente no existe .");

        var numero = $"CC-{DateTime.UtcNow:yyyyMMddHHmmssfff}-{Random.Shared.Next(100,999)}";
        if (await _uow.Cuentas.ExisteNumeroCuentaAsync(numero))
            return Conflict("Número de cuenta duplicado.");

        var cuenta = new Cuenta
        {
            ClienteId = req.ClienteId, NumeroCuenta = numero,
            TipoCuenta = req.TipoCuenta, SaldoInicial = req.SaldoInicial, Estado = true
        };
        await _uow.Cuentas.AddAsync(cuenta);
        await _uow.SaveChangesAsync();

        var dto = new CuentaDtos(cuenta.CuentaId, cuenta.NumeroCuenta, cuenta.TipoCuenta, cuenta.SaldoInicial, cuenta.Estado, cuenta.ClienteId);
        return CreatedAtAction(nameof(GetById), new { id = cuenta.CuentaId }, dto);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CuentaDtos>> GetById(Guid id)
    {
        var c = await _uow.Cuentas.GetByIdAsync(id);
        return c is null ? NotFound() : Ok(new CuentaDtos(c.CuentaId, c.NumeroCuenta, c.TipoCuenta, c.SaldoInicial, c.Estado, c.ClienteId));
    }

    [HttpGet("por-cliente/{clienteId:guid}")]
    public async Task<ActionResult<IEnumerable<CuentaDtos>>> GetByCliente(Guid clienteId)
    {
        var list = await _uow.Cuentas.GetByClienteAsync(clienteId);
        return Ok(list.Select(c => new CuentaDtos(c.CuentaId, c.NumeroCuenta, c.TipoCuenta, c.SaldoInicial, c.Estado, c.ClienteId)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CuentaDtos dto)
    {
        var c = await _uow.Cuentas.GetByIdAsync(id);
        if (c is null) return NotFound();
        c.TipoCuenta = dto.TipoCuenta; c.Estado = dto.Estado;
        _uow.Cuentas.Update(c);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonElement patch)
    {
        var c = await _uow.Cuentas.GetByIdAsync(id);
        if (c is null) return NotFound();
        if (patch.TryGetProperty("Estado", out var estado)) c.Estado = estado.GetBoolean();
        _uow.Cuentas.Update(c);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var c = await _uow.Cuentas.GetByIdAsync(id);
        if (c is null) return NotFound();
        _uow.Cuentas.Remove(c);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}