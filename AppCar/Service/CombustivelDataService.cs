using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Service
{
    public class CombustivelDataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Models.Combustivel>> GetCombustivelAsync()
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/combustivel";
                var response = await client.GetStringAsync(url);
                var combustivel = JsonConvert.DeserializeObject<List<Models.Combustivel>>(response);
                return combustivel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddCombustivelAsync(Models.Combustivel combustivel)
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/combustivel/{0}";
                var uri = new Uri(string.Format(url, combustivel.id));
                var data = JsonConvert.SerializeObject(combustivel);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir Combustivel");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateCombustivelAsync(Models.Combustivel combustivel)
        {
            string url = "http://www.testeleo.somee.com/api/combustivel/{0}";
            var uri = new Uri(string.Format(url, combustivel.id));

            var data = JsonConvert.SerializeObject(combustivel);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar Combustivel");
            }
        }

        public async Task DeleteCombustivelAsync(Models.Combustivel combustivel)
        {
            string url = "http://www.testeleo.somee.com/api/combustivel/{0}";
            var uri = new Uri(string.Format(url, combustivel.id));
            await client.DeleteAsync(uri);
        }

    }
}
