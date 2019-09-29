using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using W1.Models;

namespace W1.Controllers {
    public class AdicionarVeiculoController : ApiController {
        private readonly IAdicionarVeiculoRepositorio adicionarVeiculoRepositorio;

        public AdicionarVeiculoController() {
            adicionarVeiculoRepositorio = new AdicionarVeiculoRepositorio();
        }

        // GET: api/AdicionarVeiculo
        [HttpGet]
        public IEnumerable<W1.Models.AdicionarVeiculo> List() {
            return adicionarVeiculoRepositorio.All;
        }

        // GET: api/AdicionarVeiculo/5
        public W1.Models.AdicionarVeiculo GetAdicionarVeiculo(string Placa) {
            var veiculo = adicionarVeiculoRepositorio.Find(Placa);

            if (veiculo == null) {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return veiculo;
        }

        // POST: api/AdicionarVeiculo
        [HttpPost()]
        public void Post([FromBody]W1.Models.AdicionarVeiculo adicionarVeiculo) {
            adicionarVeiculoRepositorio.Insert(adicionarVeiculo);
        }

        // PUT: api/AdicionarVeiculo/5
        [HttpPut()]
        public void Put(string placa, [FromBody]W1.Models.AdicionarVeiculo adicionarVeiculo) {
            adicionarVeiculo.Placa = placa;
            adicionarVeiculoRepositorio.Update(adicionarVeiculo);
        }

        // DELETE: api/AdicionarVeiculo/5
        public void Delete(string Placa) {
            adicionarVeiculoRepositorio.Delete(Placa);
        }
    }
}
