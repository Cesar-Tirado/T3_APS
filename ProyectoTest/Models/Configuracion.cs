using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTest.Models
{
    public class Configuracion
    {
        public int Id { get; set; }
        public string HoraAperturaR { get; set; }

        public string HoraCierreR { get; set; }

        public string HoraAperturaD { get; set; }

        public string HoraCierreD { get; set; }

        public decimal CostoEnvio { get; set; }

        public bool PagoTarjeta { get; set; }

        public bool PagoPOS { get; set; }

        public bool PagoEfectivo { get; set; }

        public decimal PrecioMinimo { get; set; }
    }
}