using System.Collections.Generic;
using System.Globalization;

namespace CotizadorApiVertical.Models
{
    public class CatalogModel
    {
        public string Name { get; set; }
        public List<CatalogRowModel> Data { get; set; }
    }
    public class CatalogModelList
    {
        public List<CatalogRowModel> PurposeCatalog { get; set; }
        public List<CatalogRowModel> DoorTypeCatalog { get; set; }
        public List<CatalogRowModel> SheetTypeCatalog { get; set; }
        public List<CatalogRowModel> FloorTypeCatalog { get; set; }
        public List<CatalogRowTruckTypeModel> TruckTypeCatalog { get; set; }
        public List<CatalogRowEntityModel> Entities { get; set; }
        public List<CatalogRowEntityModel> Municipalities { get; set; }
        public List<CatalogRowEntityModel> Localities { get; set; }

    }
    public class CatalogRowModel
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Class { get; set; } = "sin clase";
        public string IdSyteLine { get; set; } = "n/a";
    }
    public class CatalogRowEntityModel
    {
        public int Id { get; set; } = 0;
        public int ParentId { get; set; } = -1;
        public string Name { get; set; } = string.Empty;
    }
    public class CatalogRowFregihtModel
    {
        public int Id { get; set; } = 0;
        public int LocalityId { get; set; } = 0;
        public int TruckTypeId { get; set; } = 0;
        public decimal Price { get; set; } = decimal.Zero;
    }
    public class CatalogRowTruckTypeModel
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int MinCapacity { get; set; } = 0;
        public int MaxCapacity { get; set; } = 0;
        public decimal HandlingCost { get; set; } = 0;

    }
}
