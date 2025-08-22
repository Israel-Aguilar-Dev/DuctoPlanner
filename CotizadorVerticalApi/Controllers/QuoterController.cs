using CotizadorVerticalApi.Facades;
using CotizadorVerticalApi.Models.Params;
using CotizadorVerticalApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CotizadorVerticalApi.Controllers
{
    [Route("api/quoter")]
    [ApiController]
    public class QuoterController : ControllerBase
    {
        private IQuoterFacade _service;
        public QuoterController()
        {
            _service = new QuoterService();
        }

        // GET: api/<Cotizador>
        [HttpGet]
        public ActionResult<Response> Get()
        {
            return Ok(_service.GetLastQuotes());
        }

        // GET api/<Cotizador>/5
        [HttpGet("{id}")]
        public ActionResult<Response> Get(int id)
        {
            return Ok(_service.GetQuoteById(id));
        }

        // POST api/<Cotizador>
        [HttpPost]
        public ActionResult<Response> Post([FromBody] QuoteParam quote)
        {
            return Ok(_service.InsertQuote(quote));
        }

        // PUT api/<Cotizador>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Cotizador>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
