using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_ED22.Models
{
    public class PacienteModel
    {
        [Required]
        [MinLength (5)]
        [MaxLength(30)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(13)]
        public int DPI { get; set; }

        [Required]
        [Range(0,120)]
        public int Edad { get; set; }

        [Required]
        [MaxLength(8)]
        [MinLength(8)]
        [Phone]
        public int Telefono { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime FechaUltimaConsulta { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FechaProximaConsulta { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

    }
}
