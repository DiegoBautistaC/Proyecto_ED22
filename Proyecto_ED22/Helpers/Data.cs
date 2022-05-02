using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClasesAVL;
using Proyecto_ED22.Models;

namespace Proyecto_ED22.Helpers
{
    public class Data
    {
        private static Data _instance = null;

        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }

        public ArbolAVL<PacienteModel> ArbolAVL_NombresPacientes = new ArbolAVL<PacienteModel>((PacienteModel paciente1, PacienteModel paciente2) => (paciente1.Nombre + paciente1.DPI).CompareTo(paciente2.Nombre + paciente2.DPI), (string unNombre, PacienteModel unPaciente) => unNombre.CompareTo(unPaciente.Nombre));

        public ArbolAVL<PacienteModel> ArbolAVL_DPIPacientes = new ArbolAVL<PacienteModel>((PacienteModel paciente1, PacienteModel paciente2) => paciente1.DPI.CompareTo(paciente2.DPI), (string unDPI, PacienteModel unPaciente) => unDPI.CompareTo(unPaciente.DPI));

        public ArbolAVL<PacienteModel> ArbolAVL_LimpiezaDental = new ArbolAVL<PacienteModel>((PacienteModel paciente1, PacienteModel paciente2) => (paciente1.Nombre + paciente1.DPI).CompareTo(paciente2.Nombre + paciente2.DPI), (string unNombre, PacienteModel unPaciente) => unNombre.CompareTo(unPaciente.Nombre));

        public ArbolAVL<PacienteModel> ArbolAVL_Ortodoncia = new ArbolAVL<PacienteModel>((PacienteModel paciente1, PacienteModel paciente2) => (paciente1.Nombre + paciente1.DPI).CompareTo(paciente2.Nombre + paciente2.DPI), (string unNombre, PacienteModel unPaciente) => unNombre.CompareTo(unPaciente.Nombre));

        public ArbolAVL<PacienteModel> ArbolAVL_Caries = new ArbolAVL<PacienteModel>((PacienteModel paciente1, PacienteModel paciente2) => (paciente1.Nombre + paciente1.DPI).CompareTo(paciente2.Nombre + paciente2.DPI), (string unNombre, PacienteModel unPaciente) => unNombre.CompareTo(unPaciente.Nombre));

        public ArbolAVL<PacienteModel> ArbolAVL_NoEspecificos = new ArbolAVL<PacienteModel>((PacienteModel paciente1, PacienteModel paciente2) => (paciente1.Nombre + paciente1.DPI).CompareTo(paciente2.Nombre + paciente2.DPI), (string unNombre, PacienteModel unPaciente) => unNombre.CompareTo(unPaciente.Nombre));
    }
}
