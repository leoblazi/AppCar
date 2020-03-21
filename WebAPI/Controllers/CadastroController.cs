using WebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebAPI.Controllers
{
    public class CadastroController : ApiController
    {
        private readonly ICadastroRepositorio _cadastroRepositorio;

        public CadastroController()
        {
            _cadastroRepositorio = new CadastroRepositorio();
        }

        // GET: api/Testes
        [HttpGet]
        public IEnumerable<Cadastro> List()
        {
            return _cadastroRepositorio.All;
        }

        // GET: api/Testes/5
        public Cadastro GetCadastro(int id)
        {
            var cadastro = _cadastroRepositorio.Find(id);

            if (cadastro == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return cadastro;
        }

        // POST: api/Testes
        [HttpPost()]
        public void Post([FromBody]Cadastro cadastro)
        {
            _cadastroRepositorio.Insert(cadastro);
        }

        // PUT: api/Testes/5
        [HttpPut()]
        public void Put(int id, [FromBody] Cadastro cadastro)
        {
            cadastro.id = id;
            _cadastroRepositorio.Update(cadastro);
        }

        // DELETE: api/Testes/5
        [HttpDelete()]
        public void Delete(int id)
        {
            _cadastroRepositorio.Delete(id);
        }
    }
}
