using CotizadorVerticalApi.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using CotizadorVerticalApi.Interfaces;
using CotizadorVerticalApi.Models.Params;

namespace CotizadorVerticalApi.Data
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly string _connectionString;

        public QuoteRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; ;
        }

        public QuoteInsertionResultModel InsertQuote(QuoteParam quote)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PT", quote.PT);
                parameters.Add("@NombreEjecutivo", quote.NombreEjecutivo);
                parameters.Add("@Diametro", quote.Diametro);
                parameters.Add("@PropositoId", quote.PropositoId);
                parameters.Add("@TipoLaminaId", quote.TipoLaminaId);
                parameters.Add("@SiteRef", quote.SiteRef);

                //connection.Execute("Insertar_Cotizacion", parameters, commandType: CommandType.StoredProcedure);

                //return parameters.Get<int>("@QuoteId");

                return connection.Query<QuoteInsertionResultModel>("Insertar_Cotizacion", parameters, commandType: CommandType.StoredProcedure).ToList().FirstOrDefault();
            };

            
        }

        public IEnumerable<QuoteModel> GetLastQuotes()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<QuoteModel>("Obtener_Ultimas_Cotizaciones", commandType: CommandType.StoredProcedure);
            };
            
        }

        public QuoteDetailModel GetQuoteById(int id)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                return connection.QuerySingleOrDefault<QuoteDetailModel>(
                "Obtener_Cotizacion",
                new { CotizacionId = id },
                commandType: CommandType.StoredProcedure);
            } ;
            
        }

        public void UpdateQuote(QuoteModel quote)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute("sp_UpdateQuote", quote, commandType: CommandType.StoredProcedure);
        }

        public void DeleteQuote(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute("sp_DeleteQuote", new { QuoteId = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
