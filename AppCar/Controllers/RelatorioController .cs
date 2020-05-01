using System;
using System.Collections.Generic;
using System.Text;

using AppCar.Service;

namespace AppCar.Controllers
{
    class RelatorioController
    {
        public List<Models.Relatorio> GetRelatorioByCarro(List<Models.Relatorio> lRelatorios, Models.Carro carro)
        {
            List<Models.Relatorio> relatorios = new List<Models.Relatorio>();  //Lista para retornar os relatórios do carro
            for (int i = 0; i < lRelatorios.Count; i++)
            {
                if (lRelatorios[i].carro.Trim().Equals(carro.placa.Trim()))
                {
                    relatorios.Add(lRelatorios[i]); //Adiciona os relatórios que pertencerem ao carro
                }
            }
            return relatorios;
        }

        public async void AddRelatorio(Models.Relatorio relatorio, Models.Combustivel combustivel, Models.Carro carro, int combustivelUtilizado)
        {
            switch (combustivelUtilizado)
            {
                case 0:
                    relatorio.custo = float.Parse((relatorio.kmpercorridos * combustivel.etanol / carro.kmlitro).ToString());
                    break;
                case 1:
                    relatorio.custo = float.Parse((relatorio.kmpercorridos * combustivel.gasolina / carro.kmlitro).ToString());
                    break;
                case 2:
                    relatorio.custo = float.Parse((relatorio.kmpercorridos * combustivel.diesel / carro.kmlitro).ToString());
                    break;
                case 3:
                    relatorio.custo = float.Parse((relatorio.kmpercorridos * combustivel.outro / carro.kmlitro).ToString());
                    break;
            }

            RelatorioDataService ds = new RelatorioDataService();
            await ds.AddRelatorioAsync(relatorio);
        }
    }
}