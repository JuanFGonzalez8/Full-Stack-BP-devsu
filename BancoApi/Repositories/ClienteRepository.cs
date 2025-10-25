using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ClienteRepository : IClienteRepository
{
    private readonly BancoDbContext _db;
    public ClienteRepository(BancoDbContext db) => _db = db;

    public async Task AddAsync(Cliente e) => await _db.Clientes.AddAsync(e);
    public async Task<IEnumerable<Cliente>> GetAllAsync() => await _db.Clientes.ToListAsync();
    public async Task<Cliente?> GetByIdAsync(Guid id) => await _db.Clientes.FindAsync(id);
    public void Remove(Cliente e) => _db.Clientes.Remove(e);
    public void Update(Cliente e) => _db.Clientes.Update(e);
    public Task<bool> ExisteIdentificacionAsync(string identificacion)
        => _db.Clientes.AnyAsync(x => x.Identificacion == identificacion);
}