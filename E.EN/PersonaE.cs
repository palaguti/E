using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E.EN
{
    public class PersonaE
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string NombreE { get; set; }

        [Required(ErrorMessage = "El apellido del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string ApellidoE { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoE { get; set; }

        [Required(ErrorMessage = "El sueldo es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El sueldo debe ser un número positivo.")]
        public decimal SueldoE { get; set; }

        [Required(ErrorMessage = "El estatus es obligatorio.")]
   
        public byte EstatusE { get; set; }

    }
}
