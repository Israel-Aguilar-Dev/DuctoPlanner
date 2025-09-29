using CotizadorApiVertical.Models;
using CotizadorApiVertical.Params;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotizadorApiVertical.Interfaces
{
    public interface IManPowerRepository
    {
        ResultOperationModel InsertManPower(SqlConnection connection, SqlTransaction transaction, List<HumanResource> manpower, int cotizacionId);
        ResultOperationModel GetManPower(int cotizacionId);
    }
}
