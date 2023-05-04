using System.ComponentModel.DataAnnotations.Schema;

public class TiendaArticulo {
    [Column(Order=0)]
    public int Id { get; set; }

    [Column(Order=1)]
    public virtual Tienda Tienda { get; set; }

    [Column(Order=2)]
    public virtual Articulo Articulo { get; set; }

    [Column(Order=3)]
    public DateTime Fecha { get; set; }
}