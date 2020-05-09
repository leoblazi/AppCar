using WebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebAPI.Controllers
{
    public class LembreteController : ApiController
    {
        private readonly ILembreteRepositorio _lembreteRepositorio;

        public LembreteController()
        {
            _lembreteRepositorio = new LembreteRepositorio();
        }

        // GET: api/Testes
        [HttpGet]
        public IEnumerable<Lembrete> List()
        {
            return _lembreteRepositorio.All;
        }

        // GET: api/Testes/5
        public Lembrete GetLembrete(int id)
        {
            var lembrete = _lembreteRepositorio.Find(id);

            if (lembrete == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return lembrete;
        }

        // POST: api/Testes
        [HttpPost()]
        public void Post([FromBody]Lembrete lembrete)
        {
            _lembreteRepositorio.Insert(lembrete);
        }

        // PUT: api/Testes/5
        [HttpPut()]
        public void Put(int id, [FromBody] Lembrete lembrete)
        {
            lembrete.id = id;
            _lembreteRepositorio.Update(lembrete);
        }

        // DELETE: api/Testes/5
        [HttpDelete()]
        public void Delete(int id)
        {
            _lembreteRepositorio.Delete(id);
        }
    }
}
