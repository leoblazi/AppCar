using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppCar.Service
{
    public class CadastroDataService
    {
        HttpClient client = new HttpClient();

        public async Task<List<Models.Cadastro>> GetCadastroAsync()
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/cadastro";
                var response = await client.GetStringAsync(url);
                var cadastro = JsonConvert.DeserializeObject<List<Models.Cadastro>>(response);
                return cadastro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task AddCadastroAsync(Models.Cadastro cadastro)
        {
            try
            {
                string url = "http://www.testeleo.somee.com/api/cadastro/{0}";
                var uri = new Uri(string.Format(url, cadastro.id));
                var data = JsonConvert.SerializeObject(cadastro);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir Cadastro");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateCadastroAsync(Models.Cadastro cadastro)
        {
            string url = "http://www.testeleo.somee.com/api/cadastro/{0}";
            var uri = new Uri(string.Format(url, cadastro.id));

            var data = JsonConvert.SerializeObject(cadastro);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao atualizar Cadastro");
            }
        }

        public async Task DeleteCadastroAsync(Models.Cadastro cadastro)
        {
            string url = "http://www.testeleo.somee.com/api/cadastro/{0}";
            var uri = new Uri(string.Format(url, cadastro.id));
            await client.DeleteAsync(uri);
        }

    }
}
