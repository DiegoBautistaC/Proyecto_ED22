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
        [Required(ErrorMessage = "Los {0} es requerido.")]
        [MinLength(5, ErrorMessage = "Debe ser ingresado el nombre completo, al menos un nombre y un apellido.")]
        [MaxLength(30, ErrorMessage = "El nombre completo ha excedido de 30 caracteres disponibles.")]
        [Display(Name = "Nombres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El {0} es requerido.")]
        [MinLength(13, ErrorMessage = "El {0} está conformado por 13 dígitos.")]
        [MaxLength(13, ErrorMessage = "El {0} está conformado por 13 dígitos.")]
        [Display(Name = "DPI")]
        public string DPI { get; set; }

        [Required(ErrorMessage = "La {0} es requerida.")]
        [Range(0, 120, ErrorMessage = "La {0} no es válida.")]
        [Display(Name = "Edad")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Es requerido un número de {0}.")]
        [MinLength(8, ErrorMessage = "El {0} está conformado por 8 dígitos.")]
        [MaxLength(8, ErrorMessage = "El {0} está conformado por 8 dígitos.")]
        [Display(Name = "Teléfono")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "La {0} es requerida.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de la última consulta")]
        public DateTime FechaUltimaConsulta { get; set; }


        [Display(Name = "Fecha de la próxima consulta")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ConvertEmptyStringToNull = true, ApplyFormatInEditMode = true, NullDisplayText = "00000000")]
        public DateTime? FechaProximaConsulta { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "La descripción no puede sera mayor a 500 caracteres.")]
        public string Descripcion { get; set; }

        public static bool Guardar(PacienteModel unPaciente, ref bool dpiRepetido)
        {
            if (Data.Instance.ArbolAVL_DPIPacientes.Verificacion(paciente => paciente.FechaProximaConsulta == unPaciente.FechaProximaConsulta))
            {
                if (Data.Instance.ArbolAVL_DPIPacientes.Insertar(unPaciente))
                {
                    Data.Instance.ArbolAVL_NombresPacientes.Insertar(unPaciente);
                    ModificacionArbolesAuxiliares(unPaciente);
                    return true;
                }
                dpiRepetido = true;
            }
            return false;
        }

        public static bool Editar(string dpi, int telefono, string descripcion, DateTime fecha)
        {
            if (Data.Instance.ArbolAVL_DPIPacientes.Verificacion(paciente => paciente.FechaProximaConsulta == fecha))
            {
                var PacienteDPI = Data.Instance.ArbolAVL_DPIPacientes.Encontrar(dpi);
                var PacienteNOMBRES = Data.Instance.ArbolAVL_NombresPacientes.Encontrar(PacienteDPI.Nombre + dpi);
                var PacienteLIMPIEZADENTAL = Data.Instance.ArbolAVL_LimpiezaDental.Encontrar(PacienteDPI.Nombre + dpi);
                var PacienteORTODONCIA = Data.Instance.ArbolAVL_Ortodoncia.Encontrar(PacienteDPI.Nombre + dpi);
                var PacienteCARIES = Data.Instance.ArbolAVL_Caries.Encontrar(PacienteDPI.Nombre + dpi);
                var PacienteNOESPECIFICO = Data.Instance.ArbolAVL_NoEspecificos.Encontrar(PacienteDPI.Nombre + dpi);

                if (PacienteLIMPIEZADENTAL != null)
                {
                    PacienteLIMPIEZADENTAL.Telefono = telefono;
                    PacienteLIMPIEZADENTAL.FechaProximaConsulta = fecha;
                    PacienteLIMPIEZADENTAL.Descripcion = descripcion;
                    Data.Instance.ArbolAVL_LimpiezaDental.Remover(PacienteDPI.Nombre + dpi);
                    ModificacionArbolesAuxiliares(PacienteLIMPIEZADENTAL);
                }
                if (PacienteORTODONCIA != null)
                {
                    PacienteORTODONCIA.Telefono = telefono;
                    PacienteORTODONCIA.FechaProximaConsulta = fecha;
                    PacienteORTODONCIA.Descripcion = descripcion;
                    Data.Instance.ArbolAVL_Ortodoncia.Remover(PacienteDPI.Nombre + dpi);
                    ModificacionArbolesAuxiliares(PacienteORTODONCIA);
                }
                if (PacienteCARIES != null)
                {
                    PacienteCARIES.Telefono = telefono;
                    PacienteCARIES.FechaProximaConsulta = fecha;
                    PacienteCARIES.Descripcion = descripcion;
                    Data.Instance.ArbolAVL_Caries.Remover(PacienteDPI.Nombre + dpi);
                    ModificacionArbolesAuxiliares(PacienteCARIES);
                }
                if (PacienteNOESPECIFICO != null)
                {
                    PacienteNOESPECIFICO.Telefono = telefono;
                    PacienteNOESPECIFICO.FechaProximaConsulta = fecha;
                    PacienteNOESPECIFICO.Descripcion = descripcion;
                    Data.Instance.ArbolAVL_NoEspecificos.Remover(PacienteDPI.Nombre + dpi);
                    ModificacionArbolesAuxiliares(PacienteNOESPECIFICO);
                }

                PacienteDPI.Telefono = telefono;
                PacienteDPI.FechaProximaConsulta = fecha;
                PacienteDPI.Descripcion = descripcion;

                PacienteNOMBRES.Telefono = telefono;
                PacienteNOMBRES.FechaProximaConsulta = fecha;
                PacienteNOMBRES.Descripcion = descripcion;
                return true;
            }
            return false;
        }

        static void ModificacionArbolesAuxiliares(PacienteModel unPaciente)
        {
            int meses = CalcularMeses(unPaciente);
            if (unPaciente.Descripcion == "" && meses >= 6)
            {
                Data.Instance.ArbolAVL_LimpiezaDental.Insertar(unPaciente);
            }
            if (unPaciente.Descripcion.ToUpper().Contains("ORTODONCIA") && meses >= 2)
            {
                Data.Instance.ArbolAVL_Ortodoncia.Insertar(unPaciente);
            }
            if (unPaciente.Descripcion.ToUpper().Contains("CARIES") && meses >= 4)
            {
                Data.Instance.ArbolAVL_Caries.Insertar(unPaciente);
            }
            if (unPaciente.Descripcion != "" && !unPaciente.Descripcion.ToUpper().Contains("ORTODONCIA") && !unPaciente.Descripcion.ToUpper().Contains("CARIES") && meses >= 6)
            {
                Data.Instance.ArbolAVL_NoEspecificos.Insertar(unPaciente);
            }
        }

        public static bool Eliminar(string dpi)
        {
            PacienteModel paciente = Data.Instance.ArbolAVL_DPIPacientes.Encontrar(dpi);
            Data.Instance.ArbolAVL_NombresPacientes.Remover(paciente.Nombre + dpi);
            if (Data.Instance.ArbolAVL_LimpiezaDental.Encontrar(paciente.Nombre + dpi) != null)
            {
                Data.Instance.ArbolAVL_LimpiezaDental.Remover(paciente.Nombre + dpi);
            }
            if (Data.Instance.ArbolAVL_Ortodoncia.Encontrar(paciente.Nombre + dpi) != null)
            {
                Data.Instance.ArbolAVL_Ortodoncia.Remover(paciente.Nombre + dpi);
            }
            if (Data.Instance.ArbolAVL_Caries.Encontrar(paciente.Nombre + dpi) != null)
            {
                Data.Instance.ArbolAVL_Caries.Remover(paciente.Nombre + dpi);
            }
            if (Data.Instance.ArbolAVL_NoEspecificos.Encontrar(paciente.Nombre + dpi) != null)
            {
                Data.Instance.ArbolAVL_NoEspecificos.Remover(paciente.Nombre + dpi);
            }
            Data.Instance.ArbolAVL_DPIPacientes.Remover(dpi);

            return true;
        }

        static int CalcularMeses(PacienteModel unPaciente)
        {
            int meses = 0;
            if (unPaciente.FechaUltimaConsulta.Year < DateTime.Now.Year)
            {
                int diferenciaAños = DateTime.Now.Year - unPaciente.FechaUltimaConsulta.Year;
                while (diferenciaAños > 1)
                {
                    meses += 12;
                    diferenciaAños--;
                }
                meses += 12 - unPaciente.FechaUltimaConsulta.Month;
                if (unPaciente.FechaUltimaConsulta.Day <= DateTime.Now.Day)
                {
                    meses += DateTime.Now.Month;
                }
                else
                {
                    meses += DateTime.Now.Month - 1;
                }
            }
            else
            {
                if (unPaciente.FechaUltimaConsulta.Month < DateTime.Now.Month)
                {
                    int diferenciaMeses = DateTime.Now.Month - unPaciente.FechaUltimaConsulta.Month;
                    while (diferenciaMeses > 1)
                    {
                        meses += 1;
                        diferenciaMeses--;
                    }
                    if (unPaciente.FechaUltimaConsulta.Day <= DateTime.Now.Day)
                    {
                        meses += 1;
                    }
                }
            }
            return meses;
        }
    }
}
