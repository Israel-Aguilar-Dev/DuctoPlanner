using CotizadorVerticalApi.Facades;
using CotizadorVerticalApi.Models.Params;
using CotizadorVerticalApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CotizadorVerticalApi.Controllers
{
    [Route("api/catalog")]
    [ApiController]
    public class CatalogController : Controller
    {
        private ICatalogFacade _service;
        public CatalogController()
        {
            _service = new CatalogService();
        }
        // GET: api/<Cotizador>
        [HttpGet]
        public ActionResult<Response> Get()
        {
            return Ok(_service.GetCatalogs());
        }
    }
}
