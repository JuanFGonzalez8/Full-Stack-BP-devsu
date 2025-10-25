using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICuentaRepository : IRepository<Cuenta>
{
    Task<Cuenta?> GetCuentaConMovimientosAsync(Guid cuentaId);
    Task<IEnumerable<Cuenta>> GetByClienteAsync(Guid clienteId);
    Task<bool> ExisteNumeroCuentaAsync(string numero);
}