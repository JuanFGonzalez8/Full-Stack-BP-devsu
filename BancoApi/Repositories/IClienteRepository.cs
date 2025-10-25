using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<bool> ExisteIdentificacionAsync(string identificacion);
}