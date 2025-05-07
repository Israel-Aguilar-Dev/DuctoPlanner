using CotizadorVerticalApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CotizadorVerticalApi.Controllers
{
    [Route("api/cotizador")]
    [ApiController]
    public class CotizadorController : ControllerBase
    {
        // GET: api/<Cotizador>
        [HttpGet]
        public ActionResult<Response> Get()
        {
            return Ok(new Response { Message = "success", Data = new string[] { "value1", "value2" }, StatusCode = 200 });
        }

        // GET api/<Cotizador>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"value: {id}";
        }

        // POST api/<Cotizador>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
