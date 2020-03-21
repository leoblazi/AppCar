using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppCar.Models;

namespace AppCar.Service
{
    public class RelatorioDataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Models.Relatorio>> GetRelatorioAsync()
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/relatorio";
                var response = await client.GetStringAsync(url);
                var relatorio = JsonConvert.DeserializeObject<List<Models.Relatorio>>(response);
                return relatorio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddRelatorioAsync(Models.Relatorio relatorio)
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/relatorio/{0}";
                var uri = new Uri(string.Format(url, relatorio.id));
                var data = JsonConvert.SerializeObject(relatorio);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir Relatorio");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateRelatorioAsync(Models.Relatorio relatorio)
        {
            string url = "http://www.testeleo.somee.com/api/relatorio/{0}";
            var uri = new Uri(string.Format(url, relatorio.id));

            var data = JsonConvert.SerializeObject(relatorio);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar Relatorio");
            }
        }

        public async Task DeleteRelatorioAsync(Models.Relatorio relatorio)
        {
            string url = "http://www.testeleo.somee.com/api/relatorio/{0}";
            var uri = new Uri(string.Format(url, relatorio.id));
            await client.DeleteAsync(uri);
        }

    }
}
