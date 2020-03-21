using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class CadastroRepositorio : ICadastroRepositorio
    {
        private List<Cadastro> _cadastros;

        public CadastroRepositorio()
        {
            InicializaDados();
        }

        private void InicializaDados()
        {
            _cadastros = CadastroDallHelper.GetCadastros();
        }

        public IEnumerable<Cadastro> All
        {
            get
            {
                return _cadastros;
            }
        }

        public void Delete(int id)
        {
            CadastroDallHelper.DeleteCadastro(id);
        }

        public Cadastro Find(int id)
        {
            return CadastroDallHelper.GetCadastro(id);
        }

        public void Insert(Cadastro cadastro)
        {
            if (cadastro == null)
            {
                throw new ArgumentNullException("cadastro");
            }
            CadastroDallHelper.InsertCadastro(cadastro);
        }

        public void Update(Cadastro cadastro)
        {
            if (cadastro == null)
            {
                throw new ArgumentNullException("cadastro");
            }
            CadastroDallHelper.UpdateCadastro(cadastro);
        }
    }
}