using CotizadorApiVertical.Models;
using CotizadorApiVertical.Params;
using System.Collections.Generic;

namespace CotizadorApiVertical.Interfaces
{
    public interface IQuoteRepository
    {
        QuoteInsertionResultModel InsertQuote(QuoteParam quote);
        IEnumerable<QuoteModel> GetLastQuotes();
        QuoteDetailModel GetQuoteById(int id);

    }
}
