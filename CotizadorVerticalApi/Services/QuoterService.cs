using CotizadorVerticalApi.Data;
using CotizadorVerticalApi.Facades;
using CotizadorVerticalApi.Interfaces;
using CotizadorVerticalApi.Models.Params;

namespace CotizadorVerticalApi.Services
{
    public class QuoterService : IQuoterFacade
    {
        private readonly IQuoteRepository _quoteRepository;

        public QuoterService() 
        {
            _quoteRepository = new QuoteRepository();
        }
        public async Task<Response> GetLastQuotes()
        {
            Response response = new Response();
            try
            {
                var quotes = _quoteRepository.GetLastQuotes();
                response.Data = quotes;
            }
            catch (Exception ex)
            {

                response.StatusCode = 500;
                response.Message = "No se pudieron cargar las ultimas cotizaciones";

            }
            return response;
            
        }

        public async Task<Response> GetQuoteById(int cotizacionId)
        {
            Response response = new Response();
            try
            {
                var quote = _quoteRepository.GetQuoteById(cotizacionId);
                response.Data = quote;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "No se pudo obtener la cotizacion";

            }
            return response;
        }

        public async Task<Response> InsertQuote(QuoteParam quote)
        {
            Response response = new Response();
            try
            {
                var quoteResult = _quoteRepository.InsertQuote(quote);
                response.Data = quoteResult;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "No se pudo guardar la cotizacion";

            }
            return response;
        }
    }
}
