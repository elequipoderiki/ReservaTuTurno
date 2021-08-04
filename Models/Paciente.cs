using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
  public class Paciente
  {
    [Key]
    public int IdPaciente{get; set;}

    [Required(ErrorMessage ="Debe ingresar un nombre")]
    public string Nombre {get; set;}

    [Required(ErrorMessage ="Debe ingresar un apellido")]
    public string Apellido { get; set;}

    [Required(ErrorMessage ="Debe ingresar una dirección")]
    [Display(Name ="Dirección")]
    public string Direccion {get; set;}

    [Display(Name ="Teléfono")]
    [Required(ErrorMessage ="Debe ingresar un teléfono")]
    public string Telefono {get; set; }

    [EmailAddress(ErrorMessage ="ingrese una dirección válida")]
    [Required(ErrorMessage ="Debe ingresar una dirección de correo electrónico")]
    public string Email {get; set;}

    public List<Turno>  Turno { get; set; }
  }
}