using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CuentaRepository : ICuentaRepository
{
    private readonly BancoDbContext _db;
    public CuentaRepository(BancoDbContext db) => _db = db;

    public async Task AddAsync(Cuenta e) => await _db.Cuentas.AddAsync(e);
    public async Task<IEnumerable<Cuenta>> GetAllAsync() => await _db.Cuentas.ToListAsync();
    public async Task<Cuenta?> GetByIdAsync(Guid id) => await _db.Cuentas.FindAsync(id);
    public void Remove(Cuenta e) => _db.Cuentas.Remove(e);
    public void Update(Cuenta e) => _db.Cuentas.Update(e);

    public Task<Cuenta?> GetCuentaConMovimientosAsync(Guid cuentaId) =>
        _db.Cuentas.Include(c => c.Movimientos).FirstOrDefaultAsync(c => c.CuentaId == cuentaId);

    public async Task<IEnumerable<Cuenta>> GetByClienteAsync(Guid clienteId) =>
        await _db.Cuentas.Where(c => c.ClienteId == clienteId).ToListAsync();

    public Task<bool> ExisteNumeroCuentaAsync(string numero) =>
        _db.Cuentas.AnyAsync(c => c.NumeroCuenta == numero);
}