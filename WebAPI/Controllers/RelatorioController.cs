using WebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebAPI.Controllers
{
    public class RelatorioController : ApiController
    {
        private readonly IRelatorioRepositorio _RelatorioRepositorio;

        public RelatorioController()
        {
            _RelatorioRepositorio = new RelatorioRepositorio();
        }

        // GET: api/Testes
        [HttpGet]
        public IEnumerable<Relatorio> List()
        {
            return _RelatorioRepositorio.All;
        }

        // GET: api/Testes/5
        public Relatorio GetRelatorio(int id)
        {
            var relatorio = _RelatorioRepositorio.Find(id);

            if (relatorio == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return relatorio;
        }

        // POST: api/Testes
        [HttpPost()]
        public void Post([FromBody]Relatorio relatorio)
        {
            _RelatorioRepositorio.Insert(relatorio);
        }

        // PUT: api/Testes/5
        [HttpPut()]
        public void Put(int id, [FromBody] Relatorio relatorio)
        {
            relatorio.id = id;
            _RelatorioRepositorio.Update(relatorio);
        }

        // DELETE: api/Testes/5
        [HttpDelete()]
        public void Delete(int id)
        {
            _RelatorioRepositorio.Delete(id);
        }
    }
}
