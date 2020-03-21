using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCar.Service;
using AppCar.Controllers;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionarVeiculo : ContentPage
    {
        CarroDataService ds = new CarroDataService();
        Controller controller = new Controller();
        string user;
        public AdicionarVeiculo(string login)
        {
            user = login;
            InitializeComponent();
        }

        private async void btnVoltarAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicial(user));
        }

        private async void btnCadastrarVeiculoAsync(object sender, EventArgs e)
        {
            try
            {
                List<Models.Carro> carros = await ds.GetCarroAsync(); //Lista com todos os carros
                string result; //Mensagem a ser exibida

                Models.Carro carro = new Models.Carro
                {
                    placa = txtPlaca.Text.Trim().Replace("-", ""),
                    modelo = txtModeloVeiculo.Text.Trim(),
                    dono = user,
                    tipocombustivel = txtTipoCombustivel.Text.Trim(),
                    kmatual = Convert.ToInt32(txtKmAtual.Text.Trim()),
                    kmlitro = Convert.ToInt32(txtKmLitro.Text.Trim())
                };

                result = controller.CadastrarVeiculo(carro, carros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso"))
                {
                    await ds.AddCarroAsync(carro);
                    await Navigation.PushAsync(new Inicial(user));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
        }
    }
}