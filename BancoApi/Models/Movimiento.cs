using System;


public class Movimiento
{
    public Guid MovimientoId { get; set; } = Guid.NewGuid(); 
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string TipoMovimiento { get; set; } = default!; 
    public decimal Valor { get; set; } 
    public decimal Saldo { get; set; }

    public Guid CuentaId { get; set; }
    public Cuenta Cuenta { get; set; } = default!;
}