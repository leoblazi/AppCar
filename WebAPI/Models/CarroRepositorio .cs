using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class CarroRepositorio : ICarroRepositorio
    {
        private List<Carro> _carros;

        public CarroRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _carros = CarroDallHelper.GetCarros();
        }

        public IEnumerable<Carro> All
        {
            get
            {
                return _carros;
            }
        }

        public void Delete(int id)
        {
            CarroDallHelper.DeleteCarro(id);
        }

        public Carro Find(int id)
        {
            return CarroDallHelper.GetCarro(id);
        }

        public void Insert(Carro carro)
        {
            if (carro == null)
            {
                throw new ArgumentNullException("carro");
            }
            CarroDallHelper.InsertCarro(carro);
        }

        public void Update(Carro carro)
        {
            if (carro == null)
            {
                throw new ArgumentNullException("carro");
            }
            CarroDallHelper.UpdateCarro(carro);
        }
    }
}