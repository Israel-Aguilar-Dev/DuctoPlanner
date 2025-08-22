using CotizadorApiVertical.Params;
using System.Threading.Tasks;

namespace CotizadorApiVertical.Facades
{
    public interface IQuoterFacade
    {
        Task<Response> GetLastQuotes();
        Task<Response> GetQuoteById(int cotizacionId);
        Task<Response> InsertQuote(QuoteParam quote);
    }
}
