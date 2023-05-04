using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Articulo
{
    [Column(Order = 0)]
    public int Id { get; set; }

    [MaxLength(10)]
    [Column(Order = 1)]
    public string Codigo { get; set; }

    [MaxLength(250)]
    [Column(Order = 2)]
    public string Descripcion { get; set; }

    [Column(Order = 3)]
    public decimal Precio { get; set; }

    [Column(Order=4)]
    public string Imagen { get; set; }

    [Column(Order=5)]
    public int Stock { get; set; }
}