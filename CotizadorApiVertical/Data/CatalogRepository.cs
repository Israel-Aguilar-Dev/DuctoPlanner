using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Configuration;

namespace CotizadorApiVertical.Data
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly string _connectionString;
        public CatalogRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; 
        }

        public IEnumerable<DoorTypeCatalog> GetDoorTypeCatalog()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<DoorTypeCatalog>("Obtener_Catalogo_Puertas", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<FloorTypeCatalog> GetFloorTypeCatalog()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<FloorTypeCatalog>("Obtener_Catalogo_TipoNivel", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<PurposeTypeCatalog> GetPurposeCatalog()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                return connection.Query<PurposeTypeCatalog>("Obtener_Catalogo_Proposito", commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SheetTypeCatalog> GetSheetTypeCatalog()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<SheetTypeCatalog>("Obtener_Catalogo_Lamina", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
