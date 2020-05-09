using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class LembreteRepositorio : ILembreteRepositorio
    {
        private List<Lembrete> _lembretes;

        public LembreteRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _lembretes = LembreteDallHelper.GetLembretes();
        }

        public IEnumerable<Lembrete> All
        {
            get
            {
                return _lembretes;
            }
        }

        public void Delete(int id)
        {
            LembreteDallHelper.DeleteLembrete(id);
        }

        public Lembrete Find(int id)
        {
            return LembreteDallHelper.GetLembrete(id);
        }

        public void Insert(Lembrete lembrete)
        {
            if (lembrete == null)
            {
                throw new ArgumentNullException("lembrete");
            }
            LembreteDallHelper.InsertLembrete(lembrete);
        }

        public void Update(Lembrete lembrete)
        {
            if (lembrete == null)
            {
                throw new ArgumentNullException("lembrete");
            }
            LembreteDallHelper.UpdateLembrete(lembrete);
        }
    }
}