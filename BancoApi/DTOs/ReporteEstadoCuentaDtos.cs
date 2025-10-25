using System;
using System.Collections.Generic;

public class ReporteEstadoCuentaDtos
{
    public record ReporteEstadoCuentaRequest(Guid ClienteId, DateTime Desde, DateTime Hasta);

    public record ReporteEstadoCuentaResponse(Guid ClienteId, string Nombre,
        IEnumerable<(string NumeroCuenta, decimal SaldoActual)> Cuentas,
        decimal TotalDebitos, decimal TotalCreditos, string? PdfBase64);
}