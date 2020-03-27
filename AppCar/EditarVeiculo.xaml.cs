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
    public partial class EditarVeiculo : ContentPage
    {
        CarroDataService ds;
        CarroController controller;
        Models.Carro carro;
        string user;
        public EditarVeiculo(string login, Models.Carro carro)
        {
            ds = new CarroDataService();
            controller = new CarroController();
            user = login;
            this.carro = carro;
            InitializeComponent();
            txtModelo.Text = carro.modelo.Trim();
            txtPlaca.Text = carro.placa.Trim();
            txtTipocombustivel.Text = carro.tipocombustivel.Trim();
            txtKmatual.Text = carro.kmatual.ToString().Trim();
            txtKmlitro.Text = carro.kmlitro.ToString().Trim();
        }

        private async void btnSalvarClicked(object sender, EventArgs e)
        {
            try
            {
                List<Models.Carro> carros = await ds.GetCarroAsync(); //Lista com todos os carros
                string result; //Mensagem a ser exibida

                Models.Carro novoCarro = new Models.Carro{
                    id = carro.id,
                    placa = txtPlaca.Text.Trim().ToUpper(),
                    modelo = txtModelo.Text.Trim(),
                    dono = carro.dono.Trim(),
                    tipocombustivel = txtTipocombustivel.Text.Trim(),
                    kmatual = float.Parse(txtKmatual.Text.Trim()),
                    kmlitro = float.Parse(txtKmlitro.Text.Trim()),
                    status = carro.status.Trim()
                };

                result = controller.AlterarVeiculo(novoCarro, carro, carros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso")) //Se passar na verificação
                {
                    await Navigation.PushAsync(new Veiculo(user, novoCarro));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (System.NullReferenceException ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
            
        }

        private async void btnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Veiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}