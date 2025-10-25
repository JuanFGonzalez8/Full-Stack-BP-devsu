using System.Threading.Tasks;

public class UnitOfWork : IUnitOfWork
{
    private readonly BancoDbContext _db;
    public UnitOfWork(BancoDbContext db,
        IClienteRepository clientes,
        ICuentaRepository cuentas,
        IMovimientoRepository movimientos)
    {
        _db = db;
        Clientes = clientes;
        Cuentas = cuentas;
        Movimientos = movimientos;
    }

    public IClienteRepository Clientes { get; }
    public ICuentaRepository Cuentas { get; }
    public IMovimientoRepository Movimientos { get; }

    public Task<int> SaveChangesAsync() => _db.SaveChangesAsync();
}