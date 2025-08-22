using CotizadorApiVertical.Params;
using System.Threading.Tasks;

namespace CotizadorApiVertical.Facades
{
    public interface ICatalogFacade
    {
        Task<Response> GetCatalogs();
    }
}
