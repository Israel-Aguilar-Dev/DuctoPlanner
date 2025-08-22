using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CotizadorApiVertical.Models
{
    public class FreightModel
    {
        public int FleteId { get; set; }
        public int TipoCamionId {get;set;}
        public string DescripcionCamion { get;set;}
        public string ClaseCamion { get;set;}
        public int LocalidadId {get;set;}
        public string NombreLocalidad { get;set;}
        public string NombreEntidad { get; set; }
        public decimal Precio {get;set;}

    }
}