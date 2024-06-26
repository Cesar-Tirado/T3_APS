using ProyectoTest.Logica;
using ProyectoTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoTest.Controllers
{
    public class ReservaController : Controller
    {
        private static Usuario oUsuario;
        //VISTA
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            else
                oUsuario = (Usuario)Session["Usuario"];
            return View();
        }

        //VISTA
        public ActionResult Reserva()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");
            else
                oUsuario = (Usuario)Session["Usuario"]; 
            return View();
        }

        [HttpPost]
        public JsonResult Registrar(Reserva oReserva)
        {
            int respuesta = 0;
            respuesta = ReservaLogica.Instancia.Registrar(oReserva);

            if (respuesta>0)
            {
                return Json(new { resultado = true, mensaje = " Reserva registrada exitosamente, se le enviará un correo confirmando." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { resultado = false, mensaje = "No se pudo Reservar." }, JsonRequestBehavior.AllowGet);
            }
        }

    }

}