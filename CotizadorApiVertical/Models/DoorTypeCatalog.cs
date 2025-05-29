namespace CotizadorApiVertical.Models
{
    public class PurposeTypeCatalog
    {
        public int PropositoId { get; set; }
        public string Descripcion {get;set;}
    }
    public class DoorTypeCatalog
    {
        public int TipoPuertaId { get; set; }
        public string Descripcion { get; set; }
        public string Clase { get; set; }
        public string ItemIdSyteLine { get; set; }
    }
    public class FloorTypeCatalog
    {
        public int TipoNivelId { get; set; }
        public string Descripcion { get; set; }
    }
    public class SheetTypeCatalog
    {
        public int TipoLaminaId { get; set; }
        public string Descripcion { get; set; }
    }
}
