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
            Response response = new Response();
            try
            {

            }
            catch (Exception ex)
            {
                response.Message = "No se pudo recuperar las cotizaciones";
            }
            return response;
        }
        public Response CrearCotizacion()
        {
            Response response = new Response();
            try
            {

            }
            catch (Exception ex)
            {
                response.Message = "No se pudo crear la cotizacion";
            }
            return response;
        }
        public Response ActualizarCotizacion()
        {
            Response response = new Response();
            try
            {

            }
            catch (Exception ex)
            {
                response.Message = "No se pudo actualizar la cotizacion";
            }
            return response;
        }




    }
}
