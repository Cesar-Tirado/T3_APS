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
    public class PublicController : Controller
    {
        private static Usuario oUsuario;
        //VISTA

        [HttpGet]
        public ActionResult Index()
        {
            oUsuario = new Usuario();
            oUsuario.Correo = "public@admin.com";
            oUsuario.IdUsuario = 1;
            oUsuario.Nombres = "public";
            Session["Usuario"] = oUsuario;


            List<Imagen> oLista = ProductoLogica.Instancia.ListarImagenes();
            return View(oLista);
            

        }


        public ActionResult Index2()
        {
            return View();
        }

    }

}