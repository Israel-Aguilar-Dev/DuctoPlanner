using CotizadorApiVertical.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotizadorApiVertical.Interfaces
{
    public interface IFreightRepository
    {
        FreightModel GetFreight(int localidadId, int tipoCamionId);
        ResultOperationModel InsertFreight(SqlConnection connection, SqlTransaction transaction, int localidadId, int cotizacionId);
    }
}
