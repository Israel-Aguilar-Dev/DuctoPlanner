using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace CotizadorApiVertical.Data
{
    public class FreightRepository : IFreightRepository
    {
        private readonly string _connectionString;
        public FreightRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public FreightModel GetFreight(int localidadId, int tipoCamionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@LocalidadId", localidadId);
                parameters.Add("@TipoCamionId", tipoCamionId);

                return connection.Query<FreightModel>("Obtener_Flete", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
        }
    }
}