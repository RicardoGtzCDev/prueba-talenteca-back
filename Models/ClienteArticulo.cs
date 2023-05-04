using System.ComponentModel.DataAnnotations.Schema;

public class ClienteArticulo {
    [Column(Order=0)]
    public int Id { get; set; }

    [Column(Order=1)]
    public virtual Cliente Cliente { get; set; }

    [Column(Order=2)]
    public virtual Articulo Articulo { get; set; }

    [Column(Order=3)]
    public DateTime Fecha { get; set; }
}