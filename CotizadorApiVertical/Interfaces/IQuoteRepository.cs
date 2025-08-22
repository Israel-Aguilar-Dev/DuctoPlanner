using CotizadorApiVertical.Models;
using CotizadorApiVertical.Params;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CotizadorApiVertical.Interfaces
{
    public interface IQuoteRepository
    {
        QuoteInsertionResultModel InsertQuote(SqlConnection connection, SqlTransaction transaction, QuoteParam quote);
        void InsertLevels(SqlConnection connection, SqlTransaction transaction, QuoteParam quote);
        IEnumerable<QuoteModel> GetLastQuotes();
        IEnumerable<QuoteDetailModel> GetQuoteById(int id);

    }
}
