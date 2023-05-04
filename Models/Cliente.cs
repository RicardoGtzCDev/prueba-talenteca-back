using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Cliente {
    [Column(Order=0)]
    public int Id { get; set; }

    [MaxLength(100)]
    [Column(Order=1)]
    public string Email { get; set; }

    [MaxLength(25)]
    [Column(Order=2)]
    public string Contraseña { get; set; }

    [MaxLength(100)]
    [Column(Order=3)]
    public string Nombre { get; set; }

    [MaxLength(100)]
    [Column(Order=4)]
    public string ApellidoPaterno { get; set; }

    [MaxLength(100)]
    [Column(Order=5)]
    public string ApellidoMaterno { get; set; }

    [Column(Order=6)]
    public string Direccion { get; set; }
}

