using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransporteWeb.Models;
using TransporteWeb.Repository;

namespace TransporteWeb.Controllers
{
    public class ConductorController : Controller
    {
        // GET: Conductor
        
        public ActionResult Index()
        {
            ConductorRepository cr = new ConductorRepository();

            List<Conductor> conductor = cr.getConductoresAll();

            ViewBag.resultadoParaLaVista = conductor;

            return View();
        }

        public ActionResult FormularioNuevo()
        {
            return View();
        }

        public void Insertar()
        {
            Conductor x = new Conductor();

            x.Nombre = Request["Nombre"].ToString();
            x.Cedula = Request["Cedula"].ToString();
            x.Telefono = Request["Telefono"].ToString();

            ConductorRepository cr = new ConductorRepository();

            int filasAfectadas = cr.insertConductor(x);

            Response.Redirect("/Conductor/Index");
        }

        public ActionResult FormularioUpdate()
        {

            int id = Convert.ToInt32(Request["id"].ToString());

            ConductorRepository cr = new ConductorRepository();

            Conductor x = cr.getConductorById(id);

            ViewBag.conductor = x;

            return View();
        }

        public void Actualizar()
        {
            Conductor x = new Conductor();

            x.IdConductor = Convert.ToInt32(Request["IdConductor"].ToString());
            x.Nombre = Request["Nombre"].ToString();
            x.Cedula = Request["Cedula"].ToString();
            x.Telefono = Request["Telefono"].ToString();

            ConductorRepository cr = new ConductorRepository();

            cr.updateConductor(x);


            Response.Redirect("/Conductor/Index");

        }





    }
}