using Microsoft.EntityFrameworkCore;

public class BancoDbContext : DbContext
{
    public BancoDbContext(DbContextOptions<BancoDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Cuenta> Cuentas => Set<Cuenta>();
    public DbSet<Movimiento> Movimientos => Set<Movimiento>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Cliente>().HasKey(x => x.ClienteId);
        b.Entity<Cliente>().HasIndex(x => x.Identificacion).IsUnique();

        b.Entity<Cuenta>().HasKey(x => x.CuentaId);
        b.Entity<Cuenta>().HasIndex(x => x.NumeroCuenta).IsUnique();

        b.Entity<Movimiento>().HasKey(x => x.MovimientoId);

        b.Entity<Cliente>()
            .HasMany(c => c.Cuentas)
            .WithOne(a => a.Cliente)
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<Cuenta>()
            .HasMany(a => a.Movimientos)
            .WithOne(m => m.Cuenta)
            .HasForeignKey(m => m.CuentaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}