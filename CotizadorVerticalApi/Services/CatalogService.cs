using CotizadorVerticalApi.Data;
using CotizadorVerticalApi.Facades;
using CotizadorVerticalApi.Interfaces;
using CotizadorVerticalApi.Models;
using CotizadorVerticalApi.Models.Params;

namespace CotizadorVerticalApi.Services
{
    public class CatalogService : ICatalogFacade
    {
        private readonly ICatalogRepository _catalogRepository;
        public CatalogService() 
        {
            _catalogRepository = new CatalogRepository();
        }
        public async Task<Response> GetCatalogs()
        {
            Response response = new Response();
            try
            {
                var purposeCatalog = _catalogRepository.GetPurposeCatalog();
                var doorTypeCatalog = _catalogRepository.GetDoorTypeCatalog();
                var sheetTypeCatalog = _catalogRepository.GetSheetTypeCatalog();
                var floorTypeCatalog = _catalogRepository.GetFloorTypeCatalog();
                List<CatalogModel> catalogs = new List<CatalogModel>
                {
                    new CatalogModel{ 
                        Name = "PurposeCatalog",
                        Data = purposeCatalog.Select(p => new CatalogRowModel { Id = p.PropositoId, Description = p.Descripcion }).ToList()
                    },
                    new CatalogModel{
                        Name = "DoorTypeCatalog",
                        Data = doorTypeCatalog.Select(p => new CatalogRowModel { Id = p.TipoPuertaId, Description = p.Descripcion, Class = p.Clase }).ToList()
                    },
                    new CatalogModel{
                        Name = "SheetTypeCatalog",
                        Data = sheetTypeCatalog.Select(p => new CatalogRowModel { Id = p.TipoLaminaId, Description = p.Descripcion }).ToList()
                    },
                    new CatalogModel{
                        Name = "FloorTypeCatalog",
                        Data = floorTypeCatalog.Select(p => new CatalogRowModel { Id = p.TipoNivelId, Description = p.Descripcion }).ToList()
                    },
                };
                response.Data = catalogs;
            }
            catch (Exception)
            {

                response.StatusCode = 500;
                response.Message = "Ocurrio un error al obtener los catalogos";
            }
            return response;
        }
    }
}
