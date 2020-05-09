using System;
using System.Collections.Generic;
using System.Text;

using AppCar.Service;

namespace AppCar.Controllers
{
    class LembreteController
    {
        public async void CriarLembretes(Models.Cadastro cadastro)
        {
            LembreteDataService ds = new LembreteDataService();
            Models.Lembrete lembrete = new Models.Lembrete
            {
                login = cadastro.login
            };
            lembrete.lembrete = "Se beber não dirija";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de colocar o sinto de segurança";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de abastecer o veículo";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de lavar o carro";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de encher o pneu";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de trocar o limpador";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de trocar óleo";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de colocar água";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de verificar os freios";
            await ds.AddLembreteAsync(lembrete);
            lembrete.lembrete = "Lembre-se de ir ao mecânico";
            await ds.AddLembreteAsync(lembrete);
        }
        public string GetLembrete(string login, List<Models.Lembrete> lembretes)
        {
            List<Models.Lembrete> userLembretes = new List<Models.Lembrete>();
            //Remove os lembretes que não pertencem ao usuário
            foreach (Models.Lembrete lembrete in lembretes)
            {
                if (lembrete.login.Trim().Equals(login))
                    userLembretes.Add(lembrete);
            }
            //Se houver lembrete, retorna um lembrete aleatório. Se não houver, retorna nada
            if (userLembretes.Count > 0)
            {
                Random rand = new Random();
                return userLembretes[rand.Next(0, userLembretes.Count)].lembrete;
            }
            else
                return "";
        }

        public List<Models.Lembrete> GetLembretes(string login, List<Models.Lembrete> lembretes)
        {
            List<Models.Lembrete> userLembretes = new List<Models.Lembrete>();
            //Remove os lembretes que não pertencem ao usuário
            foreach (Models.Lembrete lembrete in lembretes)
            {
                if (lembrete.login.Trim().Equals(login))
                    userLembretes.Add(lembrete);
            }
            //Retorna uma lista com os lembretes do usuário
            return userLembretes;
        }
    }
}
