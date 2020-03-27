using System;
using System.Collections.Generic;
using System.Text;

using AppCar.Service;

namespace AppCar.Controllers
{
    class CombustivelController
    {
        public Models.Combustivel GetCombustivelByCadastro(List<Models.Combustivel> combustiveis, string login)
        {
            for (int i = 0; i < combustiveis.Count; i++)
            {
                if (combustiveis[i].login.Trim().Equals(login))
                    return combustiveis[i]; //Retorna o combustível do usuário
            }
            Models.Combustivel combustivel = new Models.Combustivel
            {
                login = login,
                etanol = 0,
                diesel = 0,
                gasolina = 0,
                outro = 0
            };
            AddCombustivel(combustivel); //Se não houver combustível cadastrado para o usuário, cadastra um novo combustível

            for (int i = 0; i < combustiveis.Count; i++)
            {
                if (combustiveis[i].login.Trim().Equals(login))
                    return combustiveis[i]; //Retorna o combustível do usuário
            }
            return combustivel;
        }
        private async void AddCombustivel(Models.Combustivel combustivel)
        {
            CombustivelDataService ds = new CombustivelDataService();
            await ds.AddCombustivelAsync(combustivel);
        }
    }
}