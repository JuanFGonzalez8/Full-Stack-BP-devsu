using System;

public class ClienteDtos
{

    public record ClienteDto(Guid ClienteId, string Nombre, string Genero, int Edad,
        string Identificacion, string Direccion, string Telefono, bool Estado);

    public record CrearClienteRequest(string Nombre, string Genero, int Edad,
        string Identificacion, string Direccion, string Telefono, string Contrasena);  
  

   
}