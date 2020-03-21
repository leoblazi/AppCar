using System.Collections.Generic;

namespace WebAPI.Models {
    public interface ICarroRepositorio
    {
        IEnumerable<Carro> All { get; }
        Carro Find(int id);
        void Insert(Carro item);
        void Update(Carro item);
        void Delete(int id);
    }
}
