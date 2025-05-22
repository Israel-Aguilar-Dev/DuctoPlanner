using System.Globalization;

namespace CotizadorVerticalApi.Models
{
    public class CatalogModel
    {
        public string Name { get; set; }
        public List<CatalogRowModel> Data { get; set; }
    }
    public class CatalogRowModel 
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Class { get; set; } = "sin clase";
    }
}
