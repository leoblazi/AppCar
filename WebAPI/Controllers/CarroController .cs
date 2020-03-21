using WebAPI.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebAPI.Controllers
{
    public class CarroController : ApiController
    {
        private readonly ICarroRepositorio _carroRepositorio;

        public CarroController()
        {
            _carroRepositorio = new CarroRepositorio();
        }

        // GET: api/Testes
        [HttpGet]
        public IEnumerable<Carro> List()
        {
            return _carroRepositorio.All;
        }

        // GET: api/Testes/5
        public Carro GetCarro(int id)
        {
            var carro = _carroRepositorio.Find(id);

            if (carro == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return carro;
        }

        // POST: api/Testes
        [HttpPost()]
        public void Post([FromBody]Carro carro)
        {
            _carroRepositorio.Insert(carro);
        }

        // PUT: api/Testes/5
        [HttpPut()]
        public void Put(int id, [FromBody] Carro carro)
        {
            carro.id = id;
            _carroRepositorio.Update(carro);
        }

        // DELETE: api/Testes/5
        [HttpDelete()]
        public void Delete(int id)
        {
            _carroRepositorio.Delete(id);
        }
    }
}
