using CotizadorApiVertical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CotizadorApiVertical.Interfaces
{
    public interface IFreightRepository
    {
        FreightModel GetFreight(int localidadId, int tipoCamionId);
    }
}
