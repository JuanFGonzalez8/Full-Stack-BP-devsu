public abstract class Persona
{
    public string Nombre { get; set; } = default!;
    public string Genero { get; set; } = default!;
    public int Edad { get; set; }
    public string Identificacion { get; set; } = default!;
    public string Direccion { get; set; } = default!;
    public string Telefono { get; set; } = default!;
}