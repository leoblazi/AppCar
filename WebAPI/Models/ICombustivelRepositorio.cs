using System.Collections.Generic;

namespace WebAPI.Models {
    public interface ICombustivelRepositorio
    {
        IEnumerable<Combustivel> All { get; }
        Combustivel Find(int id);
        void Insert(Combustivel item);
        void Update(Combustivel item);
        void Delete(int id);
    }
}
