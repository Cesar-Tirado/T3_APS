using System;
using System.Collections.Generic;

namespace ProyectoTest.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public string NombreLocal { get; set; }
        public bool Activo { get; set; }
        public string NombreCliente { get; set; }
        public string TipoDocumento { get; set; }
        public string Cantidad { get; set; }
        public string NumeroDocumento { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        public string Motivo { get; set; }
        public string Observacion { get; set; }
        public string Anio { get; set; }
        public string Mes { get; set; }
        public string Dias { get; set; }
        public string IdReservaLocal { get; set; }
        public FechaModel oFecha { get; set; }        
        
    }
}