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
            return View(Data.Instance.ArbolAVL_NombresPacientes);
        }

        // GET: PacienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PacienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PacienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
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
    }
}
