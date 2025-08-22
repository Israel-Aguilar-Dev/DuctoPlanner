using CotizadorVerticalApi.Models;

namespace CotizadorVerticalApi.Interfaces
{
    public interface ICatalogRepository
    {
        IEnumerable<PurposeTypeCatalog> GetPurposeCatalog();
        IEnumerable<SheetTypeCatalog> GetSheetTypeCatalog();
        IEnumerable<FloorTypeCatalog> GetFloorTypeCatalog();
        IEnumerable<DoorTypeCatalog> GetDoorTypeCatalog();
    }
}
