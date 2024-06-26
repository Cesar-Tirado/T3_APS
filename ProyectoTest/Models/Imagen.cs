using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTest.Models
{
    public class Imagen
    {
        public int Id { get; set; }
        public string RutaImagen { get; set; }
        public string base64 { get; set; }
        public string extension { get; set; }
        public bool Activo { get; set; }
    }
}