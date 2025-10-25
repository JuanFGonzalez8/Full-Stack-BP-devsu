using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMovimientoRepository : IRepository<Movimiento>
{
    Task<IEnumerable<Movimiento>> ListarAsync(Guid? cuentaId, DateTime? desde, DateTime? hasta);
    Task<decimal> TotalDebitosDiaAsync(Guid cuentaId, DateTime diaUtc);
}