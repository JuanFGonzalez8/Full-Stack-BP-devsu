using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static ClienteDtos;

[ApiController]
[Route("clientes")]
public class ClientesController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    public ClientesController(IUnitOfWork uow) => _uow = uow;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
    {
        var list = await _uow.Clientes.GetAllAsync();
        return Ok(list.Select(c => new ClienteDto(c.ClienteId, c.Nombre, c.Genero, c.Edad,
            c.Identificacion, c.Direccion, c.Telefono, c.Estado)));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClienteDto>> GetById(Guid id)
    {
        var c = await _uow.Clientes.GetByIdAsync(id);
        return c is null ? NotFound() : Ok(new ClienteDto(c.ClienteId, c.Nombre, c.Genero, c.Edad,
            c.Identificacion, c.Direccion, c.Telefono, c.Estado));
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Create([FromBody] CrearClienteRequest req)
    {
        if (await _uow.Clientes.ExisteIdentificacionAsync(req.Identificacion))
            return Conflict("Identificación ya registrada.");

        var cliente = new Cliente
        {
            Nombre = req.Nombre, Genero = req.Genero, Edad = req.Edad,
            Identificacion = req.Identificacion, Direccion = req.Direccion,
            Telefono = req.Telefono,
            ContrasenaHash = req.Contrasena,
            Estado = true
        };
        await _uow.Clientes.AddAsync(cliente);
        await _uow.SaveChangesAsync();

        var dto = new ClienteDto(cliente.ClienteId, cliente.Nombre, cliente.Genero, cliente.Edad,
            cliente.Identificacion, cliente.Direccion, cliente.Telefono, cliente.Estado);
        return CreatedAtAction(nameof(GetById), new { id = cliente.ClienteId }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClienteDto dto)
    {
        var c = await _uow.Clientes.GetByIdAsync(id);
        if (c is null) return NotFound();
        c.Nombre = dto.Nombre; c.Genero = dto.Genero; c.Edad = dto.Edad;
        c.Direccion = dto.Direccion; c.Telefono = dto.Telefono; c.Estado = dto.Estado;
        _uow.Clientes.Update(c);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] JsonElement patch)
    {
        var c = await _uow.Clientes.GetByIdAsync(id);
        if (c is null) return NotFound();
        if (patch.TryGetProperty("Estado", out var estado)) c.Estado = estado.GetBoolean();
        if (patch.TryGetProperty("Telefono", out var tel)) c.Telefono = tel.GetString() ?? c.Telefono;
        _uow.Clientes.Update(c);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var c = await _uow.Clientes.GetByIdAsync(id);
        if (c is null) return NotFound();
        _uow.Clientes.Remove(c);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}