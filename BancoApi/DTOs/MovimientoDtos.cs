using System;

public class MovimientoDtos
{
      public record MovimientoDto(Guid MovimientoId, DateTime Fecha, string TipoMovimiento,
        decimal Valor, decimal Saldo, Guid CuentaId);

    public record CrearMovimientoRequest(Guid CuentaId, string TipoMovimiento, decimal Valor);
}