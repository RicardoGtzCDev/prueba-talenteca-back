public class TiendaArticulo {
    public int Id { get; set; }
    public virtual Tienda Tienda { get; set; }
    public virtual Articulo Articulo { get; set; }
    public DateTime Fecha { get; set; }
}