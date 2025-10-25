using System;

public class CuentaDtos
{
    private Guid cuentaId;
    private string numeroCuenta;
    private string tipoCuenta;
    private decimal saldoInicial;
    private bool estado;
    private Guid clienteId;

    public CuentaDtos(Guid cuentaId, string numeroCuenta, string tipoCuenta, decimal saldoInicial, bool estado, Guid clienteId)
    {
        this.cuentaId = cuentaId;
        this.numeroCuenta = numeroCuenta;
        this.tipoCuenta = tipoCuenta;
        this.saldoInicial = saldoInicial;
        this.estado = estado;
        this.clienteId = clienteId;
    }

    public string TipoCuenta { get; internal set; }
    public bool Estado { get; internal set; }

    public record CuentaDto(Guid CuentaId, string NumeroCuenta, string TipoCuenta,
            decimal SaldoInicial, bool Estado, Guid ClienteId);

    public record CrearCuentaRequest(Guid ClienteId, string TipoCuenta, decimal SaldoInicial);
}
