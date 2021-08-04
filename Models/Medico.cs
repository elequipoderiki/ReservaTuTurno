using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
  public class Medico
  {
    [Key]
    public int IdMedico { get; set; }

    [Required(ErrorMessage ="Debe ingresar un nombre")]
    public string Nombre { get; set; }

    [Required(ErrorMessage ="Debe ingresar un apellido")]
    public string Apellido { get; set; }

    [Display(Name ="Dirección")]
    [Required(ErrorMessage ="Debe ingresar una dirección")]
    public string Direccion { get; set; }

    [Display(Name ="Teléfono")]
    [Required(ErrorMessage ="Debe ingresar un teléfono")]
    public string Telefono { get; set; }

    [EmailAddress(ErrorMessage ="ingrese una dirección válida")]
    [Required(ErrorMessage ="Debe ingresar una dirección de correo electrónico")]
    public string Email { get; set; }
 
    [Display(Name ="Inicio de horario de atención")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString ="{0:hh:mm tt}",  ApplyFormatInEditMode =true)]
    public DateTime HorarioAtencionDesde { get; set; }
 
    [Display(Name ="Fin de horario de atención  ")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString ="{0:hh:mm tt}", ApplyFormatInEditMode =true)] 
    public DateTime HorarioAtencionHasta { get; set; }
 
    public List<MedicoEspecialidad> MedicoEspecialidad { get; set; }

    public List<Turno> Turno { get; set; }
  }
}