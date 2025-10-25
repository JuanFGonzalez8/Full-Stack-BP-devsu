using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly BancoDbContext _db;
    public MovimientoRepository(BancoDbContext db) => _db = db;

    public async Task AddAsync(Movimiento e) => await _db.Movimientos.AddAsync(e);
    public async Task<IEnumerable<Movimiento>> GetAllAsync() => await _db.Movimientos.ToListAsync();
    public async Task<Movimiento?> GetByIdAsync(Guid id) => await _db.Movimientos.FindAsync(id);
    public void Remove(Movimiento e) => _db.Movimientos.Remove(e);
    public void Update(Movimiento e) => _db.Movimientos.Update(e);

    public async Task<IEnumerable<Movimiento>> ListarAsync(Guid? cuentaId, DateTime? desde, DateTime? hasta)
    {
        var q = _db.Movimientos.AsQueryable();
        if (cuentaId.HasValue) q = q.Where(m => m.CuentaId == cuentaId.Value);
        if (desde.HasValue) q = q.Where(m => m.Fecha >= desde.Value);
        if (hasta.HasValue) q = q.Where(m => m.Fecha <= hasta.Value);
        return await q.OrderByDescending(m => m.Fecha).ToListAsync();
    }

    public async Task<decimal> TotalDebitosDiaAsync(Guid cuentaId, DateTime diaUtc)
    {
        var desde = diaUtc.Date;
        var hasta = desde.AddDays(1);
        return await _db.Movimientos
            .Where(m => m.CuentaId == cuentaId && m.Fecha >= desde && m.Fecha < hasta && m.Valor < 0)
            .SumAsync(m => Math.Abs(m.Valor));
    }
}