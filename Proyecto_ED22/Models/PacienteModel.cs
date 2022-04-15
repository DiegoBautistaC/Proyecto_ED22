using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Proyecto_ED22.Helpers;

namespace Proyecto_ED22.Models
{
    public class PacienteModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Nombre { get; set; }

        [Required]
        [MinLength(13)]
        [MaxLength(13)]
        public string DPI { get; set; }

        [Required]
        [Range(0, 120)]
        public int Edad { get; set; }

        [Required]
        [Phone]
        [MaxLength(8)]
        [MinLength(8)]
        public int Telefono { get; set; }

        [Required]
        public DateTime FechaUltimaConsulta { get; set; }

        public DateTime FechaProximaConsulta { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }

        public static bool Guardar(PacienteModel unPaciente)
        {
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(unPaciente);
            Data.Instance.ArbolAVL_NombresPacientes.Insertar(unPaciente);
            return true;
        }
    }
}
