using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Service
{
    public class LembreteDataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Models.Lembrete>> GetLembreteAsync()
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/lembrete";
                var response = await client.GetStringAsync(url);
                var lembrete = JsonConvert.DeserializeObject<List<Models.Lembrete>>(response);
                return lembrete;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddLembreteAsync(Models.Lembrete lembrete)
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/lembrete/{0}";
                var uri = new Uri(string.Format(url, lembrete.id));
                var data = JsonConvert.SerializeObject(lembrete);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir Lembrete");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateLembreteAsync(Models.Lembrete lembrete)
        {
            string url = "http://www.testeleo.somee.com/api/lembrete/{0}";
            var uri = new Uri(string.Format(url, lembrete.id));

            var data = JsonConvert.SerializeObject(lembrete);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar Lembrete");
            }
        }

        public async Task DeleteLembreteAsync(Models.Lembrete lembrete)
        {
            string url = "http://www.testeleo.somee.com/api/lembrete/{0}";
            var uri = new Uri(string.Format(url, lembrete.id));
            await client.DeleteAsync(uri);
        }

    }
}
