using System;
using System.Collections.Generic;


public class Cuenta
{
    public Guid CuentaId { get; set; } = Guid.NewGuid(); 
    public string NumeroCuenta { get; set; } = default!; 
    public string TipoCuenta { get; set; } = default!;
    public decimal SaldoInicial { get; set; }
    public bool Estado { get; set; } = true;

    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; } = default!;
    public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}