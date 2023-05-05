public class ClienteArticuloResponse
{    public int Id { get; set; }
    public virtual ClienteResponse Cliente { get; set; }
    public virtual Articulo Articulo { get; set; }
    public DateTime Fecha { get; set; }
}