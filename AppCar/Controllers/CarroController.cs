using System;
using System.Collections.Generic;
using System.Text;

using AppCar.Service;

namespace AppCar.Controllers
{
    class CarroController
    {
        public string AdcionarVeiculo(Models.Carro carro, List<Models.Carro> carros)
        {
            //Verifica se há campos vazios
            if (carro.placa.Equals("") || carro.modelo.Equals("") || carro.tipocombustivel.Equals("") || carro.kmatual.Equals("") || carro.kmlitro.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica se o carro já está cadastrado
            for (int i = 0; i < carros.Count; i++)
            {
                if (carros[i].placa.Trim().Equals(carro.placa))
                    return "Erro;Carro já cadastrado;OK";
            }
            //Verifica se a placa contém todos os caracteres
            if (carro.placa.Trim().Length<8)
                return "Erro;A placa deve conter todos os caracteres;OK";

            //Verifica se Km rodados é negativo
            if (Convert.ToInt32(carro.kmatual)<0)
                return "Erro;\"Km(s) rodados\" não pode ser negativo;OK";

            //Verifica se Km por litro é negativo
            if (Convert.ToInt32(carro.kmlitro) <= 0)
                return "Erro;\"Km(s) por litro\" não pode ser menor nem igual a zero;OK";

            return "Sucesso;Carro cadastrado com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }

        public string AlterarVeiculo(Models.Carro novoCarro, Models.Carro carro, List<Models.Carro> carros)
        {
            //Verifica se há campos vazios
            if (novoCarro.placa.Equals("") || novoCarro.modelo.Equals("") || novoCarro.tipocombustivel.Equals("") || novoCarro.kmatual.Equals("") || novoCarro.kmlitro.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            if (!novoCarro.placa.Equals(carro.placa.Trim()))
            {
                //Verifica se a placa já está cadastrado
                for (int i = 0; i < carros.Count; i++)
                {
                    if (carros[i].placa.Trim().Equals(novoCarro.placa))
                        return "Erro;Placa já cadastrada;OK";
                }
            }
            //Verifica se Km rodados é negativo
            if (Convert.ToInt32(novoCarro.kmatual) < 0)
                return "Erro;\"Km atual\" não pode ser negativo;OK";

            //Verifica se Km por litro é negativo
            if (Convert.ToInt32(novoCarro.kmlitro) <= 0)
                return "Erro;\"Km por litro\" não pode ser menor nem igual a zero;OK";

            UpdateCarro(novoCarro, carro);
            return "Sucesso;Carro alterado com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }
        private async void UpdateCarro(Models.Carro novoCarro, Models.Carro carro)
        {
            CarroDataService ds = new CarroDataService();
            if (!novoCarro.placa.Equals(carro.placa.Trim())) //Se a placa foi alterada
            {
                await ds.AddCarroAsync(novoCarro); //Adciona o carro com a placa alterado

                RelatorioDataService relds = new RelatorioDataService();
                List<Models.Relatorio> relatorios = await relds.GetRelatorioAsync();

                for (int i = 0; i < relatorios.Count; i++)
                {
                    if (relatorios[i].carro.Trim().Equals(carro.placa.Trim())) //Para todo relatorio com a placa alterada, muda para a nova placa
                    {
                        Models.Relatorio relatorio = new Models.Relatorio
                        {
                            id = relatorios[i].id,
                            carro = novoCarro.placa,
                            endinicial = relatorios[i].endinicial,
                            endfinal = relatorios[i].endfinal,
                            datainicial = relatorios[i].datainicial,
                            datafinal = relatorios[i].datafinal,
                            kmpercorridos = relatorios[i].kmpercorridos,
                            custo = relatorios[i].custo
                        };
                        await relds.UpdateRelatorioAsync(relatorio);
                    }
                }
                await ds.DeleteCarroAsync(carro); //Deleta o carro com a placa antiga
            }
        }

        public List<Models.Carro> GetCarroByCadastro(List<Models.Carro> carros, string user)
        {
            List<Models.Carro> userCars = new List<Models.Carro>(); //Lista para retornar os carros do usuário
            for (int i=0; i<carros.Count; i++)
            {
                if (carros[i].dono.Trim().Equals(user))
                    userCars.Add(carros[i]); //Adiciona os carros que pertencerem ao usuário
            }
            return userCars;
        }

        public async void DeleteCarro(Models.Carro carro)
        {
            RelatorioDataService rds = new RelatorioDataService();
            foreach (Models.Relatorio relatorio in await rds.GetRelatorioAsync())
            {
                if (relatorio.carro.Equals(carro.placa))
                {
                    await rds.DeleteRelatorioAsync(relatorio);
                }
            }
            CarroDataService ds = new CarroDataService();
            await ds.DeleteCarroAsync(carro);
        }
    }
}
