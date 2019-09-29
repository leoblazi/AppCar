using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using AppCar.Models;

namespace AppCar.Service {
    class DataService {

        HttpClient client = new HttpClient();
        public async Task<List<CadastroLogin>> GetCadastroLoginAsync() {
            try {
                string url = "http://www.testeleo.somee.com/api/";
                var response = await client.GetStringAsync(url);
                var logins = JsonConvert.DeserializeObject<List<CadastroLogin>>(response);
                return logins;
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task AddCadastroLoginAsync(CadastroLogin cadastroLogin) {
            try {
                string url = "http://www.testeleo.somee.com/api/{0}";
                var uri = new Uri(string.Format(url, cadastroLogin.Login));
                var data = JsonConvert.SerializeObject(cadastroLogin);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode) {
                    throw new Exception("Erro ao incluir Login");
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

    }
}
