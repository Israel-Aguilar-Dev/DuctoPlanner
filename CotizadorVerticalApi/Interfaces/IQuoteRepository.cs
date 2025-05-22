using CotizadorVerticalApi.Models;
using CotizadorVerticalApi.Models.Params;

namespace CotizadorVerticalApi.Interfaces
{
    public interface IQuoteRepository
    {
        QuoteInsertionResultModel InsertQuote(QuoteParam quote);
        IEnumerable<QuoteModel> GetLastQuotes();
        QuoteDetailModel GetQuoteById(int id);

    }
}
