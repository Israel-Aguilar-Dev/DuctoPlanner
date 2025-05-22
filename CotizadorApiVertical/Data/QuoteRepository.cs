using CotizadorApiVertical.Models;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Params;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace CotizadorVerticalApi.Data
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly string _connectionString;

        public QuoteRepository( )
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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

    }
}
