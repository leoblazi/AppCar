using System.Collections.Generic;

namespace WebAPI.Models {
    public interface IRelatorioRepositorio
    {
        IEnumerable<Relatorio> All { get; }
        Relatorio Find(int id);
        void Insert(Relatorio item);
        void Update(Relatorio item);
        void Delete(int id);
    }
}
