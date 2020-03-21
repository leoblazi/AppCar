using WebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebAPI.Controllers
{
    public class CombustivelController : ApiController
    {
        private readonly ICombustivelRepositorio _combustivelRepositorio;

        public CombustivelController()
        {
            _combustivelRepositorio = new CombustivelRepositorio();
        }

        // GET: api/Testes
        [HttpGet]
        public IEnumerable<Combustivel> List()
        {
            return _combustivelRepositorio.All;
        }

        // GET: api/Testes/5
        public Combustivel GetCombustivel(int id)
        {
            var combustivel = _combustivelRepositorio.Find(id);

            if (combustivel == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return combustivel;
        }

        // POST: api/Testes
        [HttpPost()]
        public void Post([FromBody]Combustivel combustivel)
        {
            _combustivelRepositorio.Insert(combustivel);
        }

        // PUT: api/Testes/5
        [HttpPut()]
        public void Put(int id, [FromBody] Combustivel combustivel)
        {
            combustivel.id = id;
            _combustivelRepositorio.Update(combustivel);
        }

        // DELETE: api/Testes/5
        [HttpDelete()]
        public void Delete(int id)
        {
            _combustivelRepositorio.Delete(id);
        }
    }
}
