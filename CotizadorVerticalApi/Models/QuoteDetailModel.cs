namespace CotizadorVerticalApi.Models
{
    public class QuoteDetailModel
    {
        public string PT { get; set; } = string.Empty;
        public int CotizacionId { get; set; } = -1;
        public DateTime Fecha { get; set; } = DateTime.MinValue;
        public int PropositoId { get; set; } = -1;
        public string Proposito { get; set; } = string.Empty;
        public int TipoLaminaId { get; set; } = -1;
        public string TipoLamina { get; set; } = string.Empty;
        public int NumeroVersion { get; set; } = -1;
        public decimal Altura { get; set; } = -1;
        public int Cantidad { get; set; } = 0; 
        public bool NecesitaPuerta { get; set; } = false;
        public int TipoPuertaId { get; set; } = -1;
        public string TipoPuerta { get; set; } = string.Empty;
        public int TipoNivelId { get; set; } = -1;
        public string TipoNivel { get; set; } = string.Empty;
        public bool NecesitaChimenea { get; set; } = false;

    }
}
