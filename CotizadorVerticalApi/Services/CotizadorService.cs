using CotizadorVerticalApi.Models;
namespace CotizadorVerticalApi.Services
{
    public class CotizadorService
    {
        private CotizadorService _service;
        public CotizadorService(CotizadorService service) 
        {
            _service = service;
        }

        public Response ObtenerCotizacion() 
        {
            Response response = new Response();
            try
            {

            }
            catch (Exception ex)
            {
                response.Message = "No se pudo recuperar la cotizacion";
            }
            return response;
        }
        public Response ObtenerCotizaciones() 
        { 

        }
        public Response CrearCotizacion()
        {

        }
        public Response ActualizarCotizacion()
        {
            
        }




    }
}
