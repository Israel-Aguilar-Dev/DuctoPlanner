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
    public class TruckTypeCatalog
    {
        public int TipoCamionId { get; set; }
        public string Descripcion { get; set; }
        public string Clase { get; set; }
        public int CapacidadMinima { get; set; }
        public int CapacidadMaxima { get; set; }
        public decimal CostoManiobra { get; set; } 
    }
    public class EntityCatalog 
    {
        public int EntidadId { get; set; }
        public string Nombre { get; set; }
    }
    public class MunicipalityCatalog
    {
        public int MunicipioId { get; set; }
        public int EntidadId { get; set; }
        public string Nombre { get; set; }
    }
    public class LocalityCatalog
    {
        public int LocalidadId { get; set; }
        public int MunicipioId { get; set; }
        public string Nombre { get; set; }
    }
    public class ResourceCatalog 
    {
        public int RecursoId { get; set; }
        public string Descripcion { get; set; }
        public decimal SalarioPorJornada { get; set; }
    }
    public class ResourceTypeCatalog
    {
        public int TipoRecursoId { get; set; }
        public string Descripcion { get;set; }
    }
    public class RentabilitiesCatalog
    {
        public int RentabilidadMOId { get; set; }
        public decimal Rentabilidad { get; set; }
        public string Descripcion { get; set; }
    }

}
