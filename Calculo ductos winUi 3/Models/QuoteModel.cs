
using System;
using System.Collections.Generic;

namespace Calculo_ductos_winUi_3.Models
{
    public class QuoteModel
    {
        public int Id { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.MinValue;
        public string PT { get; set; } = string.Empty;
    }
    public class QuoteInsertionResultModel
    {
        public int Id { get; set; } = 0;
        public int Version { get; set; } = 0;
    }
    public class QuoteDetailModel
    {
        public int CotizacionId { get; set; } = -1;
        public string PT { get; set; } = string.Empty;
        public string NombreEjecutivo { get; set; } = "No asignado" ;
        public int NumeroVersion { get; set; } = 0;
        public string Diametro { get; set; } = string.Empty;
        public int TipoLaminaId { get; set; } = 0;
        public int PropositoId { get; set; } = 0;
        public string SiteRef { get; set; } = string.Empty;
        public bool NecesitaAspersor { get; set; } = false;
        public bool NecesitaSistemaDD { get; set; } = false;
        public List<FloorDetailModel> Niveles { get; set; }


    }
    public class FloorDetailModel
    {
        public int TipoNivelId { get; set; } = 0;
        public int Cantidad {get;set;} = 0;
        public decimal Altura {get;set;} = 0;
        public bool NecesitaPuerta {get;set;} = false;
        public bool NecesitaChimenea {get;set;} = false;
        public bool NecesitaAntiImpactos { get; set; } = false;
        public int TipoPuertaId {get;set;} = 0;
        public int TipoDescargaId { get; set; } = 0;
    }
}
