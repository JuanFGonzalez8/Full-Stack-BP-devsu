using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUnitOfWork
{
    IClienteRepository Clientes { get; }
    ICuentaRepository Cuentas { get; }
    IMovimientoRepository Movimientos { get; }
    Task<int> SaveChangesAsync();
}