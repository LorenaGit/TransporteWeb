using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransporteWeb.Models
{
    public class Conductor
    {
        public int IdConductor { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
    }
}