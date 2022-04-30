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
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Diego Bautista",
                DPI = "3050203740117",
                Edad = 19,
                Telefono = 55630778,
                FechaUltimaConsulta = Convert.ToDateTime("08/05/2021"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = ""
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Victor Bautista",
                DPI = "5548624862483",
                Edad = 21,
                Telefono = 81532491,
                FechaUltimaConsulta = Convert.ToDateTime("06/07/2018"),
                Descripcion = "Ortodoncia"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Douglas Salazar",
                DPI = "1954342605182",
                Edad = 15,
                Telefono = 40001428,
                FechaUltimaConsulta = Convert.ToDateTime("24/12/2016"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = "Colocación de Braquets"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Manuel Bautista",
                DPI = "7756243381590",
                Edad = 45,
                Telefono = 55114248,
                FechaUltimaConsulta = Convert.ToDateTime("06/03/2022"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = "Trataimiento de ortodoncia"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Nidia Cruz",
                DPI = "8854362977185",
                Edad = 46,
                Telefono = 45821658,
                FechaUltimaConsulta = Convert.ToDateTime("13/03/2020"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = "Caries"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Maximus Alexander",
                DPI = "2241568720014",
                Edad = 8,
                Telefono = 94521806,
                FechaUltimaConsulta = Convert.ToDateTime("01/06/2021"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = ""
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Lourdes del Socorro",
                DPI = "5306221058010",
                Edad = 75,
                Telefono = 18452625,
                FechaUltimaConsulta = Convert.ToDateTime("04/08/2018"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = "Relleno de diente"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Ezequiel Cruz",
                DPI = "4215388010101",
                Edad = 81,
                Telefono = 45526135,
                FechaUltimaConsulta = Convert.ToDateTime("04/11/2002"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = "Tratamiento de ortodoncia"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Ricardo García",
                DPI = "4865221486770",
                Edad = 80,
                Telefono = 88401735,
                FechaUltimaConsulta = Convert.ToDateTime("07/05/2019"),
                FechaProximaConsulta = Convert.ToDateTime("09/05/2022"),
                Descripcion = "Seguimiento de caries"
            });
            PacienteModel.Guardar(new PacienteModel
            {
                Nombre = "Victor Bautista",
                DPI = "8746895214056",
                Edad = 77,
                Telefono = 18654250,
                FechaUltimaConsulta = Convert.ToDateTime("04/04/2022"),
                Descripcion = "Caries"
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
                if (Convert.ToDateTime(collection["FechaUltimaConsulta"]).CompareTo(DateTime.Today) > 0)
                {
                    nuevoPaciente.FechaUltimaConsulta = new DateTime();
                    ViewBag.Message = "La fecha de ultima consulta no es valida.";
                    return View(nuevoPaciente);
                }
                if (fechaProximaConsulta != "")
                {
                    if (Convert.ToDateTime(collection["FechaProximaConsulta"]).CompareTo(DateTime.Today) <= 0)
                    {
                        ViewBag.Message = "No se puede registrar una cita para día anteriores o el actual.";
                        if (Convert.ToDateTime(collection["FechaProximaConsulta"]).CompareTo(Convert.ToDateTime(collection["FechaUltimaConsulta"])) == 0)
                        {
                            ViewBag.Message = "La fecha de ultima consulta no puede ser igual a la de proxima consulta.";
                        }
                        nuevoPaciente.FechaProximaConsulta = new DateTime();
                        return View(nuevoPaciente);
                    }
                    nuevoPaciente.FechaProximaConsulta = Convert.ToDateTime(collection["FechaProximaConsulta"]);
                }
                var validacion = PacienteModel.Guardar(nuevoPaciente);
                if (validacion)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No es posible almacenar al paciente, porque el dia esta lleno o el DPI ya existe en el sistema.";
                return View(nuevoPaciente);
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inesperado.";
                return RedirectToAction(nameof(Index));
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
                if (DateTime.Today.CompareTo(Convert.ToDateTime(collection["FechaProximaConsulta"])) > 0)
                {
                    ViewBag.Message = "La nueva fecha para proxima consulta no es valida.";
                    return View(Data.Instance.ArbolAVL_DPIPacientes.Encontrar(id));
                }
                var validacion = PacienteModel.Editar(id, Convert.ToInt32(collection["Telefono"]), Convert.ToDateTime(collection["FechaProximaConsulta"]));
                if (validacion)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "No ha sido posible reagendar la cita ya que el dia esta lleno.";
                return View(Data.Instance.ArbolAVL_DPIPacientes.Encontrar(id));
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inesperado.";
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
                ViewBag.Message = "No se ha completado/cancelado correctamente la cita.";
                return View();
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inesperado.";
                return View();
            }
        }
        
        public ActionResult BuscarNombre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarNombre(string nombre)
        {
            try
            {
                if(nombre != null)
                {
                    var paciente = Data.Instance.ArbolAVL_NombresPacientes.Encontrar(nombre);
                    if (paciente != null)
                    {
                        return View(paciente);
                    }
                    ViewBag.Message = "No se ha encontrado a " + nombre + " en el sistema.";
                    return View();
                }
                else
                {
                    ViewBag.Message = "Debe ingresar un nombre para iniciar la busqueda.";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inesperado.";
                return View();
            }
        }

        public ActionResult BuscarDPI()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarDPI(string dpi)
        {
            try
            {
                if (dpi != null)
                {
                    var paciente = Data.Instance.ArbolAVL_DPIPacientes.Encontrar(dpi);
                    if (paciente != null)
                    {
                        return View(paciente);
                    }
                    ViewBag.Message = "No se encontro al paciente con DPI " + dpi;
                    return View();
                }
                else
                {
                    ViewBag.Message = "Debe ingresar un DPI para inicial la busqueda.";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "Ha ocurrido un error inesperado.";
                return View();
            }
        }

        public ActionResult ObtenerPacientesLimpiezaDental()
        {
            if (Data.Instance.ArbolAVL_LimpiezaDental.Vacio())
            {
                ViewBag.Message = "Todavia no hay pacientes que necesiten una limpieza dental.";
            }
            return View(Data.Instance.ArbolAVL_LimpiezaDental);
        }
        public ActionResult ObtenerPacientesOrtodoncia()
        {
            if (Data.Instance.ArbolAVL_Ortodoncia.Vacio())
            {
                ViewBag.Message = "Todavia non hay paciente que necesiten tratamiento de ortodoncia.";
            }
            return View(Data.Instance.ArbolAVL_Ortodoncia);
        }
        public ActionResult ObtenerPacientesCaries()
        {
            if (Data.Instance.ArbolAVL_Caries.Vacio())
            {
                ViewBag.Message = "Todavia no hay pacientes que necesiten tratamiento de caries.";
            }
            return View(Data.Instance.ArbolAVL_Caries);
        }
        public ActionResult ObtenerPacientesNoEspecifico()
        {
            if (Data.Instance.ArbolAVL_NoEspecificos.Vacio())
            {
                ViewBag.Message = "Todavia no hay pacientes que necesiten de algun tratamiento especifico.";
            }
            return View(Data.Instance.ArbolAVL_NoEspecificos);
        }
    }
}
