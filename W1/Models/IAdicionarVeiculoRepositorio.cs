using System.Collections.Generic;

namespace W1.Models {
    public interface IAdicionarVeiculoRepositorio {

        IEnumerable<AdicionarVeiculo> All { get; }
        AdicionarVeiculo Find(string Placa);
        void Insert(AdicionarVeiculo veiculo);
        void Update(AdicionarVeiculo veiculo);
        void Delete(string Placa);
    }
}
