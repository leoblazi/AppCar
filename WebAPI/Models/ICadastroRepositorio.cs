using System.Collections.Generic;

namespace WebAPI.Models {
    public interface ICadastroRepositorio
    {
        IEnumerable<Cadastro> All { get; }
        Cadastro Find(int id);
        void Insert(Cadastro item);
        void Update(Cadastro item);
        void Delete(int id);
    }
}
