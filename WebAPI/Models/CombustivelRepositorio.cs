using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class CombustivelRepositorio : ICombustivelRepositorio
    {
        private List<Combustivel> _combustiveis;

        public CombustivelRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _combustiveis = CombustivelDallHelper.GetCombustiveis();
        }

        public IEnumerable<Combustivel> All
        {
            get
            {
                return _combustiveis;
            }
        }

        public void Delete(int id)
        {
            CombustivelDallHelper.DeleteCombustivel(id);
        }

        public Combustivel Find(int id)
        {
            return CombustivelDallHelper.GetCombustivel(id);
        }

        public void Insert(Combustivel combustivel)
        {
            if (combustivel == null)
            {
                throw new ArgumentNullException("combustivel");
            }
            CombustivelDallHelper.InsertCombustivel(combustivel);
        }

        public void Update(Combustivel combustivel)
        {
            if (combustivel == null)
            {
                throw new ArgumentNullException("combustivel");
            }
            CombustivelDallHelper.UpdateCombustivel(combustivel);
        }
    }
}