public class ClienteArticulo {
    public int Id { get; set; }
    public virtual Cliente Cliente { get; set; }
    public virtual Articulo Articulo { get; set; }
    public DateTime Fecha { get; set; }
}