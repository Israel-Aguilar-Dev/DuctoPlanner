using CotizadorApiVertical.Facades;
using CotizadorApiVertical.Params;
using CotizadorApiVertical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CotizadorVerticalApi.Controllers
{
    [Route("api/catalog")]
    public class CatalogController : ApiController
    {
        private ICatalogFacade _service;
        public CatalogController()
        {
            _service = new CatalogService();
        }
        // GET: api/<Cotizador>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_service.GetCatalogs());
        }
    }
}
