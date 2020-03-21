using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppCar.Models;

namespace AppCar.Service
{
    public class CarroDataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Models.Carro>> GetCarroAsync()
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/carro";
                var response = await client.GetStringAsync(url);
                var carro = JsonConvert.DeserializeObject<List<Models.Carro>>(response);
                return carro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddCarroAsync(Models.Carro carro)
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/carro/{0}";
                var uri = new Uri(string.Format(url, carro.id));
                var data = JsonConvert.SerializeObject(carro);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir Carro");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateCarroAsync(Models.Carro carro)
        {
            string url = "http://www.testeleo.somee.com/api/carro/{0}";
            var uri = new Uri(string.Format(url, carro.id));

            var data = JsonConvert.SerializeObject(carro);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar Carro");
            }
        }

        public async Task DeleteCarroAsync(Models.Carro carro)
        {
            string url = "http://www.testeleo.somee.com/api/carro/{0}";
            var uri = new Uri(string.Format(url, carro.id));
            await client.DeleteAsync(uri);
        }

    }
}
