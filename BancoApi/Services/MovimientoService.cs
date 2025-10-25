using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MovimientoService
{
    private const decimal LIMITE_DIARIO_DEBITO = 1000m;
    private readonly IUnitOfWork _uow;
    public MovimientoService(IUnitOfWork uow) => _uow = uow;

    public async Task<(bool ok, string? error, Movimiento? mov)> RegistrarMovimientoAsync(Guid cuentaId, string tipo, decimal valor)
    {
        var cuenta = await _uow.Cuentas.GetCuentaConMovimientosAsync(cuentaId);
        if (cuenta is null || !cuenta.Estado) return (false, "Cuenta no disponible.", null);

        var saldoActual = cuenta.SaldoInicial + cuenta.Movimientos.Sum(m => m.Valor);

        var esDebito = tipo.Equals("Debito", StringComparison.OrdinalIgnoreCase);
        var esCredito = tipo.Equals("Credito", StringComparison.OrdinalIgnoreCase);
        if (!esDebito && !esCredito) return (false, "TipoMovimiento inválido.", null);

        var valorNormalizado = esCredito ? Math.Abs(valor) : -Math.Abs(valor);

        if (esDebito)
        {
            if (saldoActual + valorNormalizado < 0) return (false, "Saldo no disponible.", null);
            var totalDebitosHoy = await _uow.Movimientos.TotalDebitosDiaAsync(cuenta.CuentaId, DateTime.UtcNow);
            if (totalDebitosHoy + Math.Abs(valorNormalizado) > LIMITE_DIARIO_DEBITO)
                return (false, "Cupo diario Excedido", null);
        }

        var nuevoSaldo = saldoActual + valorNormalizado;
        var mov = new Movimiento
        {
            CuentaId = cuenta.CuentaId,
            Fecha = DateTime.UtcNow,
            TipoMovimiento = esDebito ? "Debito" : "Credito",
            Valor = valorNormalizado,
            Saldo = nuevoSaldo
        };

        await _uow.Movimientos.AddAsync(mov);
        await _uow.SaveChangesAsync();
        return (true, null, mov);
    }
}