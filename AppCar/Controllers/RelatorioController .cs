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
                if (lRelatorios[i].carro.Trim().Equals(carro.placa))
                    relatorios.Add(lRelatorios[i]); //Adiciona os relatórios que pertencerem ao carro
            }
            return relatorios;
        }
    }
}