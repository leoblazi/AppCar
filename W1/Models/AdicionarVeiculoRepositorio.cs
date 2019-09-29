using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W1.Models {
    public class AdicionarVeiculoRepositorio : IAdicionarVeiculoRepositorio {

        private List<AdicionarVeiculo> adicionarVeiculos;

        public AdicionarVeiculoRepositorio() {
            InicializaDados();
        }

        private void InicializaDados() {
            adicionarVeiculos = DalHelper.GetAdicionarVeiculos();
        }

        public IEnumerable<AdicionarVeiculo> All {
            get {
                return adicionarVeiculos;
            }
        }

        public void Delete(string Placa) {
            DalHelper.DeleteVeiculo(Placa);
        }

        public AdicionarVeiculo Find(string Placa) {
            return DalHelper.GetVeiculo(Placa);
        }

        public void Insert(AdicionarVeiculo adicionarVeiculo) {
            if (adicionarVeiculo == null) {
                throw new ArgumentNullException("adicionarVeiculo");
            }
            DalHelper.InsertVeiculo(adicionarVeiculo);
        }

        public void Update(AdicionarVeiculo adicionarVeiculo) {
            if (adicionarVeiculo == null) {
                throw new ArgumentNullException("adicionarVeiculo");
            }
            DalHelper.UpdateVeiculo(adicionarVeiculo);
        }
    }
}