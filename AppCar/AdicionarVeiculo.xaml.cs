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
        CarroDataService ds;
        CarroController controller;
        string user;
        public AdicionarVeiculo(string login)
        {
            InitializeComponent();
            ds = new CarroDataService();
            controller = new CarroController();
            user = login;
        }

        private void btnVoltarAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }

        private async void btnCadastrarVeiculoAsync(object sender, EventArgs e)
        {
            try
            {
                List<Models.Carro> carros = await ds.GetCarroAsync(); //Lista com todos os carros
                string result; //Mensagem a ser exibida

                Models.Carro carro = new Models.Carro //Carro a ser adicionado
                {
                    placa = txtPlaca.Text.Trim().ToUpper(), //Deixa os números da placa em maiúsculo 
                    modelo = txtModeloVeiculo.Text.Trim(),
                    dono = user.Trim(),
                    tipocombustivel = txtTipoCombustivel.Text.Trim(),
                    kmatual = float.Parse(txtKmAtual.Text.Trim()),
                    kmlitro = float.Parse(txtKmLitro.Text.Trim()),
                    status = "Parado" //alterado para sincronizar com o banco
                };

                result = controller.AdcionarVeiculo(carro, carros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso")) //Se passar na verificação
                {
                    await ds.AddCarroAsync(carro); //Adiciona o carro ao banco
                    await Navigation.PushAsync(new Inicial(user));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
        }
    }
}