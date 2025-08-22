using CotizadorApiVertical.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotizadorApiVertical.Facades
{
    public interface IFreightFacade
    {
        Task<Response> GetFreight(int localidadId, int tipoCamionId);
    }
}
