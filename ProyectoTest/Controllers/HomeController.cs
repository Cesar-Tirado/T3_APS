using ProyectoTest.Models;
using ProyectoTest.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Web.Caching;
using System.Runtime.Caching;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ProyectoTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Categoria()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Marca()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Tiendas()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Reserva()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }


        public ActionResult Producto()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Tienda()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Clientes()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Configuracion()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Imagenes()
        {
            if (Session["Usuario"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }


        [HttpGet]
        public JsonResult ListarConfiguracion()
        {
            List<Configuracion> oLista = new List<Configuracion>();
            oLista = TiendasLogica.Instancia.ListarConf();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        
        }


        [HttpGet]
        public JsonResult ListarImagenes()
        {
            List<Imagen> oLista = new List<Imagen>();
            oLista = ProductoLogica.Instancia.ListarImagenes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult GuardarConfiguracion(Configuracion objeto)
        {
            bool respuesta = false;
            respuesta = TiendasLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarCategoria() {
            List<Categoria> oLista = new List<Categoria>();
            oLista = CategoriaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdCategoria == 0) ? CategoriaLogica.Instancia.Registrar(objeto) : CategoriaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            respuesta = CategoriaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarMarca()
        {
            List<Marca> oLista = new List<Marca>();
            oLista = MarcaLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarMarca(Marca objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdMarca == 0) ? MarcaLogica.Instancia.Registrar(objeto) : MarcaLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            respuesta = MarcaLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarTiendas()
        {
            List<Tiendas> oLista = new List<Tiendas>();
            oLista = TiendasLogica.Instancia.Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarClientes()
        {
            List<Cliente> oLista = new List<Cliente>();
            oLista = UsuarioLogica.Instancia.ListarClientes();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarLocales()
        {
            List<Reserva> oLista = new List<Reserva>();
            oLista = ReservaLogica.Instancia.ListarLocal();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTiendas(Tiendas objeto)
        {
            bool respuesta = false;
            respuesta = (objeto.IdTiendas == 0) ? TiendasLogica.Instancia.Registrar(objeto) : TiendasLogica.Instancia.Modificar(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarLocales(Reserva objeto, FechaModel oFecha)
        {
            bool respuesta = false;
            respuesta = (objeto.Id == 0) ? ReservaLogica.Instancia.RegistrarLocal(objeto) : ReservaLogica.Instancia.ModificarLocal(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarFechas(Reserva objeto)
        {
            bool respuesta = false;
            respuesta = ReservaLogica.Instancia.RegistrarFecha(objeto);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerFecha(Reserva oFecha)
        {
            List<Reserva> oLista = new List<Reserva>();
            oLista = ReservaLogica.Instancia.ObtenerFechas(oFecha);
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarLocales(int id)
        {
            bool respuesta = false;
            respuesta = ReservaLogica.Instancia.EliminarLocal(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTiendas(int id)
        {
            bool respuesta = false;
            respuesta = TiendasLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProducto()
        {

            List<Producto> oLista = new List<Producto>();
            oLista = ProductoLogica.Instancia.Listar();
            oLista = oLista.Select(o => new Producto
            {
                IdProducto = o.IdProducto,
                Nombre = o.Nombre,
                Descripcion = o.Descripcion,
                OpcionesConCosto = o.OpcionesConCosto,
                OpcionesSinCosto = o.OpcionesSinCosto,
                OpcionExcluyente = o.OpcionExcluyente,
                MaxOpcionesSinCosto = o.MaxOpcionesSinCosto,
                oMarca = o.oMarca,
                oCategoria = o.oCategoria,
                Precio = o.Precio,
                RutaImagen = o.RutaImagen,
                //base64 = utilidades.convertirBase64(Server.MapPath(o.RutaImagen)),
                extension = Path.GetExtension(o.RutaImagen).Replace(".", ""),
                Activo = o.Activo,
            }).ToList();
            var json = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpGet]
        public JsonResult ListarImagenesNuevo()
        {

            List<Imagen> oLista = new List<Imagen>();
            oLista = ProductoLogica.Instancia.ListarImagenes();
            oLista = oLista.Select(o => new Imagen
            {
                Id = o.Id,
                RutaImagen = o.RutaImagen,
                Activo = o.Activo,
                extension = Path.GetExtension(o.RutaImagen).Replace(".", ""),
                base64 = utilidades.convertirBase64(Server.MapPath(o.RutaImagen))
            }).ToList();
            var json = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpGet]
        public JsonResult ListarProducto2()
        {

            List<Producto> oLista = new List<Producto>();
            oLista = ProductoLogica.Instancia.Listar2();
            oLista = oLista.Select(o => new Producto
            {
                IdProducto = o.IdProducto,
                Nombre = o.Nombre,
                oMarca = o.oMarca,
                oCategoria = o.oCategoria,
                Precio = o.Precio,
                Activo = o.Activo,
            }).ToList();
            var json = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }


        [HttpPost]
        public JsonResult GuardarImagenInicial(string objeto, HttpPostedFileBase imagenArchivo)
        {
            Response oresponse = new Response() { resultado = true, mensaje = "" };

            try
            {
                Imagen oImagen = new Imagen();
                oImagen = JsonConvert.DeserializeObject<Imagen>(objeto);

                string GuardarEnRuta = "~/Imagenes/Productos";
                string physicalPath = Server.MapPath("~/Imagenes/Productos");

                if (!Directory.Exists(physicalPath))
                    Directory.CreateDirectory(physicalPath);

                if (oImagen.Id == 0)
                {
                    
                }

                else
                {
                    oresponse.resultado = ProductoLogica.Instancia.ModificarImagenInicial(oImagen);
                }

                if (imagenArchivo != null && oImagen.Id != 0)
                {
                    string extension = Path.GetExtension(imagenArchivo.FileName);
                    GuardarEnRuta = GuardarEnRuta + "/" + oImagen.Id.ToString() + extension;
                    oImagen.RutaImagen = GuardarEnRuta;

                    imagenArchivo.SaveAs(physicalPath + "/" + oImagen.Id.ToString() + extension);

                    oresponse.resultado = ProductoLogica.Instancia.ActualizarRutaImagenInicial(oImagen);
                }

            }
            catch (Exception e)
            {
                oresponse.resultado = false;
                oresponse.mensaje = e.Message;
            }

            return Json(oresponse, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult ApagarTienda()
        {
            var exito = ProductoLogica.Instancia.ApagarTienda();

            if (exito)
            {
                return Json(new { success = true, message = "La tienda ha sido apagada correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "No se pudo apagar la tienda. Inténtalo de nuevo." });
            }
        }

        [HttpPost]
        public ActionResult EncenderTienda()
        {
            var exito = ProductoLogica.Instancia.EncenderTienda();

            if (exito)
            {
                return Json(new { success = true, message = "La tienda ha sido encendida correctamente." });
            }
            else
            {
                return Json(new { success = false, message = "No se pudo encender la tienda. Inténtalo de nuevo." });
            }
        }


        [HttpPost]
        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase imagenArchivo)
        {
            Response oresponse = new Response() { resultado = true, mensaje = "" };

            try
            {
                Producto oProducto = new Producto();
                oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

                string GuardarEnRuta = "~/Imagenes/Productos";
                string physicalPath = Server.MapPath("~/Imagenes/Productos");

                if (!Directory.Exists(physicalPath))
                    Directory.CreateDirectory(physicalPath);

                if (oProducto.IdProducto == 0)
                {
                    int id = ProductoLogica.Instancia.Registrar(oProducto);
                    oProducto.IdProducto = id;
                    oresponse.resultado = oProducto.IdProducto == 0 ? false : true;
                    if (oProducto.IdProducto == 0)
                    {
                        oresponse.mensaje = "Hubo un error en línea 183, no se encontró el producto" + oProducto.IdProducto;
                    }
                }
                else
                {
                    oresponse.resultado = ProductoLogica.Instancia.Modificar(oProducto);
                    oresponse.mensaje = "Hubo un error en línea 188, no se encontró el producto" + oProducto.IdProducto;
                }

                if (imagenArchivo != null && oProducto.IdProducto != 0)
                {
                    string extension = Path.GetExtension(imagenArchivo.FileName);
                    GuardarEnRuta = GuardarEnRuta+"/"+  oProducto.IdProducto.ToString() + extension;
                    oProducto.RutaImagen = GuardarEnRuta;

                    imagenArchivo.SaveAs(physicalPath + "/" + oProducto.IdProducto.ToString() + extension);

                    oresponse.resultado = ProductoLogica.Instancia.ActualizarRutaImagen(oProducto);
                }

            }
            catch (Exception e)
            {
                oresponse.resultado = false;
                oresponse.mensaje = e.Message;
            }

            return Json(oresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            respuesta = ProductoLogica.Instancia.Eliminar(id);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }

    public class Response {

        public bool resultado { get; set; }
        public string mensaje { get; set; }
    }
}