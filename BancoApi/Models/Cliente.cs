using System;
using System.Collections.Generic;


public class Cliente : Persona
{
    public Guid ClienteId { get; set; } = Guid.NewGuid(); 
    public string ContrasenaHash { get; set; } = default!;
    public bool Estado { get; set; } = true;
    public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}