using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        private List<Relatorio> _relatorios;

        public RelatorioRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _relatorios = RelatorioDallHelper.GetRelatorios();
        }

        public IEnumerable<Relatorio> All
        {
            get
            {
                return _relatorios;
            }
        }

        public void Delete(int id)
        {
            RelatorioDallHelper.DeleteRelatorio(id);
        }

        public Relatorio Find(int id)
        {
            return RelatorioDallHelper.GetRelatorio(id);
        }

        public void Insert(Relatorio relatorio)
        {
            if (relatorio == null)
            {
                throw new ArgumentNullException("relatorio");
            }
            RelatorioDallHelper.InsertRelatorio(relatorio);
        }

        public void Update(Relatorio relatorio)
        {
            if (relatorio == null)
            {
                throw new ArgumentNullException("relatorio");
            }
            RelatorioDallHelper.UpdateRelatorio(relatorio);
        }
    }
}