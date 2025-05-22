using CotizadorVerticalApi.Models.Params;

namespace CotizadorVerticalApi.Facades
{
    public interface IQuoterFacade
    {
        Task<Response> GetLastQuotes();
        Task<Response> GetQuoteById(int cotizacionId);
        Task<Response> InsertQuote(QuoteParam quote);
    }
}
