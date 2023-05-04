using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tienda {

    [Column(Order=0)]
    public int Id { get; set; }

    [MaxLength(250)]
    [Column(Order=1)]
    public string Sucursal { get; set; }

    [Column(Order=2)]
    public string Direccion { get; set; }
}