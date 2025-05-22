namespace CotizadorVerticalApi.Models.Params
{
    public class QuoteParam
    {
        public string PT { get; set; } = string.Empty;
        public string NombreEjecutivo { get; set; } = "NO ASIGNADO";
        public string Diametro { get; set; } = "24\"";
        public int PropositoId { get; set; } = 0;
        public int TipoLaminaId { get; set; } = 0;
        public string SiteRef { get; set; } = "VERS";

    }
}
