using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_ED22.Helpers;
using Proyecto_ED22.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_ED22.Controllers
{
    public class PacienteController : Controller
    {
        // GET: PacienteController
        public ActionResult Index()
        {
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Maximus",
                DPI = "1234567894512",
                Edad = 19,
                FechaUltimaConsulta = Convert.ToDateTime("05/06/2020"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = "Caries"
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Fernando",
                DPI = "3050203740117",
                Edad = 20,
                FechaUltimaConsulta = Convert.ToDateTime("08/01/2018"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = ""
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Gloria",
                DPI = "30514820123145",
                Edad = 22,
                FechaUltimaConsulta = Convert.ToDateTime("24/08/2017"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = "Ortodoncia"
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Enrique",
                DPI = "6802340975126",
                Edad = 40,
                FechaUltimaConsulta = Convert.ToDateTime("26/04/2019"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = ""
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Fernanda",
                DPI = "1235840625195",
                Edad = 19,
                FechaUltimaConsulta = Convert.ToDateTime("30/08/2021"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = "Limpieza dental"
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Ingrid",
                DPI = "4251352012350",
                Edad = 50,
                FechaUltimaConsulta = Convert.ToDateTime("06/07/2018"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = ""
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Ingrid",
                DPI = "4251352012350",
                Edad = 50,
                FechaUltimaConsulta = Convert.ToDateTime("06/07/2018"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = ""
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Ingrid",
                DPI = "4251352012350",
                Edad = 50,
                FechaUltimaConsulta = Convert.ToDateTime("06/07/2018"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = ""
            });
            Data.Instance.ArbolAVL_DPIPacientes.Insertar(new PacienteModel
            {
                Nombre = "Ingrid",
                DPI = "4251352012350",
                Edad = 50,
                FechaUltimaConsulta = Convert.ToDateTime("06/07/2018"),
                FechaProximaConsulta = Convert.ToDateTime("29/04/2022"),
                Descripcion = ""
            });
            return View(Data.Instance.ArbolAVL_NombresPacientes);
        }

        // GET: PacienteController/Details/5
        public ActionResult Details(string id)
        {
            return View(Data.Instance.ArbolAVL_DPIPacientes.Encontrar(id));
        }

        // GET: PacienteController/Create
        public ActionResult Create()
        {
            return View(new PacienteModel());
        }

        // POST: PacienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                string fechaProximaConsulta = collection["FechaProximaConsulta"];
                PacienteModel nuevoPaciente = new PacienteModel
                {
                    Nombre = collection["Nombre"],
                    DPI = collection["DPI"],
                    Edad = Convert.ToInt32(collection["Edad"]),
                    Telefono = Convert.ToInt32(collection["Telefono"]),
                    FechaUltimaConsulta = Convert.ToDateTime(collection["FechaUltimaConsulta"]),
                    Descripcion = collection["Descripcion"]
                };
                if (fechaProximaConsulta != "")
                {
                    if (Convert.ToDateTime(collection["FechaProximaConsulta"]).CompareTo(Convert.ToDateTime(collection["FechaUltimaConsulta"])) <= 0)
                    {
                        return View();
                    }
                    nuevoPaciente.FechaProximaConsulta = Convert.ToDateTime(collection["FechaProximaConsulta"]);
                }
                var validacion = PacienteModel.Guardar(nuevoPaciente);
                if (validacion)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Edit/5
        public ActionResult Edit(string id)
        {
            var paciente = Data.Instance.ArbolAVL_DPIPacientes.Encontrar(id);
            return View(paciente);
        }

        // POST: PacienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                if (PacienteModel.Editar(id, Convert.ToInt32(collection["Telefono"]), Convert.ToDateTime(collection["FechaProximaConsulta"])))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Delete/5
        public ActionResult Delete(string id)
        {
            return View(Data.Instance.ArbolAVL_DPIPacientes.Encontrar(id));
        }

        // POST: PacienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                if (PacienteModel.Eliminar(id))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult BuscarNombre(string nombre)
        {
            try
            {
                if(nombre != null)
                {
                    var paciente = Data.Instance.ArbolAVL_NombresPacientes.Encontrar(nombre);
                    return View(paciente);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BuscarDPI(string dpi)
        {
            try
            {
                if (dpi != null)
                {
                    var paciente = Data.Instance.ArbolAVL_DPIPacientes.Encontrar(dpi);
                    return View(paciente);
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ObtenerPacientesLimpiezaDental()
        {
            return View(Data.Instance.ArbolAVL_LimpiezaDental);
        }
        public ActionResult ObtenerPacientesOrtodoncia()
        {
            return View(Data.Instance.ArbolAVL_Ortodoncia);
        }
        public ActionResult ObtenerPacientesCaries()
        {
            return View(Data.Instance.ArbolAVL_Caries);
        }
        public ActionResult ObtenerPacientesNoEspecifico()
        {
            return View(Data.Instance.ArbolAVL_NoEspecificos);
        }
    }
}
