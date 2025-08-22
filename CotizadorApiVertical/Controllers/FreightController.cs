using CotizadorApiVertical.Facades;
using CotizadorApiVertical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CotizadorApiVertical.Controllers
{
    public class FreightController : ApiController
    {
        private IFreightFacade _service;
        public FreightController()
        {
            _service = new FreightService();
        }

        // GET: api/Freight/5
        public IHttpActionResult Get(int idLocalidad, int idTipoCamion)
        {
            return Ok(_service.GetFreight(idLocalidad, idTipoCamion));
        }
    }
}
