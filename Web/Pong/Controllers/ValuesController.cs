using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Aolh.Pong.Libreria.Negocio;
using Aolh.Pong.Libreria.Modelos;

namespace Pong.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Consultas Post([FromBody]Consultas value)
        {
            return PongServicio.ConsultasRealizadas();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
