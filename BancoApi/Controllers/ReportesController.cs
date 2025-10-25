using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static ReporteEstadoCuentaDtos;

[ApiController]
[Route("reportes")]
public class ReportesController : ControllerBase
{
    private readonly BancoDbContext _db;
    public ReportesController(BancoDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<ReporteEstadoCuentaResponse>> EstadoCuenta(
        [FromQuery] Guid clienteId, [FromQuery] DateTime desde, [FromQuery] DateTime hasta, [FromQuery] bool incluirPdf = true)
    {
        var cliente = await _db.Clientes.FindAsync(clienteId);
        if (cliente is null) return NotFound("Cliente no existe.");

        var cuentas = await _db.Cuentas.Include(c => c.Movimientos)
            .Where(c => c.ClienteId == clienteId).ToListAsync();

        var cuentasSaldo = cuentas.Select(c => (c.NumeroCuenta, c.SaldoInicial + c.Movimientos.Sum(m => m.Valor))).ToList();

        var movimientos = cuentas.SelectMany(c => c.Movimientos).Where(m => m.Fecha >= desde && m.Fecha <= hasta);
        var totalDebitos = movimientos.Where(m => m.Valor < 0).Sum(m => Math.Abs(m.Valor));
        var totalCreditos = movimientos.Where(m => m.Valor > 0).Sum(m => m.Valor);

        string? pdfBase64 = null;
        if (incluirPdf)
        {
            // Generación básica (sustituir por librería PDF real: QuestPDF / iText7)
            var contenido = $"Estado de cuenta\nCliente: {cliente.Nombre}\nPeriodo: {desde:d} - {hasta:d}\n" +
                            $"Total Débitos: {totalDebitos}\nTotal Créditos: {totalCreditos}\n" +
                            string.Join("\n", cuentasSaldo.Select(cs => $"{cs.NumeroCuenta}: {cs.Item2}"));
            var bytes = System.Text.Encoding.UTF8.GetBytes(contenido); // placeholder
            pdfBase64 = Convert.ToBase64String(bytes);
        }

        var resp = new ReporteEstadoCuentaResponse(cliente.ClienteId, cliente.Nombre, cuentasSaldo, totalDebitos, totalCreditos, pdfBase64);
        return Ok(resp);
    }
}