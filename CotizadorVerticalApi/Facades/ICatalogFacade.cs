using CotizadorVerticalApi.Models.Params;

namespace CotizadorVerticalApi.Facades
{
    public interface ICatalogFacade
    {
        Task<Response> GetCatalogs();
    }
}
