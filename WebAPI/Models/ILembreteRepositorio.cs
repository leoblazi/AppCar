using System.Collections.Generic;

namespace WebAPI.Models
{
    public interface ILembreteRepositorio
    {
        IEnumerable<Lembrete> All { get; }
        Lembrete Find(int id);
        void Insert(Lembrete item);
        void Update(Lembrete item);
        void Delete(int id);
    }
}