using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Models;
using CotizadorApiVertical.Params;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CotizadorApiVertical.Data
{
    public class ManPowerRepository : IManPowerRepository
    {
        private readonly string _connectionString;
        public ManPowerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public ResultOperationModel GetManPower(int cotizacionId)
        {
            var result = new ResultOperationModel();
            try
            {

                using (var connection = new SqlConnection(_connectionString))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@CotizacionId", cotizacionId);
                    result.Success = true;
                    result.Message = "Se consulto correctamente";
                    result.Data = connection.Query<HumanResource>("Obtener_Mano_De_Obra", parameters, commandType: CommandType.StoredProcedure).ToList();

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrio un error en GetManPower: {ex.Message}";
            }
            return result;
        }

        public ResultOperationModel InsertManPower(SqlConnection connection, SqlTransaction transaction,List<HumanResource> manpower, int cotizacionId)
        {
            var result = new ResultOperationModel();
            try
            {
                foreach (var humanResource in manpower)
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@CotizacionId", cotizacionId);
                    parameters.Add("@RecursoId", humanResource.RecursoId);
                    parameters.Add("@TipoRecursoId", humanResource.TipoRecursoId);

                    connection.Execute(
                        "Insertar_Mano_De_Obra",
                        parameters,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );
                }
                result.Success = true;
                result.Message = "Se agregaron correctamente los recursos humanos";
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Ocurrio un error en InsertManPower {ex.Message}";
            }
            return result;
        }
    }
}