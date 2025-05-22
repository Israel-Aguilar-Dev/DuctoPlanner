using CotizadorApiVertical.Facades;
using CotizadorApiVertical.Params;
using CotizadorVerticalApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CotizadorApiVertical.Controllers
{
    [Route("api/quoter")]
    public class QuoterController : ApiController
    {
        private IQuoterFacade _service;
        public QuoterController()
        {
            _service = new QuoterService();
        }

        // GET: api/<Cotizador>
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_service.GetLastQuotes());
        }

        // GET api/<Cotizador>/5
        //[HttpGet("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_service.GetQuoteById(id));
        }

        // POST api/<Cotizador>
        [HttpPost]
        public IHttpActionResult Post([FromBody] QuoteParam quote)
        {
            return Ok(_service.InsertQuote(quote));
        }

        //// PUT api/<Cotizador>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<Cotizador>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
