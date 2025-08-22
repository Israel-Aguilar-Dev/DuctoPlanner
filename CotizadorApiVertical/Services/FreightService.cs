using CotizadorApiVertical.Data;
using CotizadorApiVertical.Facades;
using CotizadorApiVertical.Interfaces;
using CotizadorApiVertical.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CotizadorApiVertical.Services
{
    public class FreightService : IFreightFacade
    {
        private readonly IFreightRepository _freightRepository;
        public FreightService()
        {
            _freightRepository = new FreightRepository();
        }
        public async Task<Response> GetFreight(int localidadId, int tipoCamionId)
        {

            Response response = new Response();
            try
            {
                var detail = _freightRepository.GetFreight(localidadId, tipoCamionId);
                var Freight = new
                {
                    FreightId = detail.FleteId,
                    TruckTypeId = detail.TipoCamionId,
                    TruckDescription = detail.DescripcionCamion,
                    TruckName = detail.ClaseCamion,
                    LocalityId = detail.LocalidadId,
                    LocalityName = detail.NombreLocalidad,
                    EntityName = detail.NombreEntidad,
                    Price = detail.Precio
                };
                response.Data = Freight;
                response.StatusCode = 200;
                response.Message = "Éxito";
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "No se pudo obtener el flete";
            }
            return response;
        }
    }
}