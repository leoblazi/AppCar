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
    public partial class Veiculo : ContentPage
    {
        Models.Carro carro;
        string user;
        public Veiculo(string login, Models.Carro carro)
        {
            user = login;
            this.carro = carro;
            InitializeComponent();
            txtModelo.Text = "Modelo: " + carro.modelo;
            txtPlaca.Text = "Placa: " + carro.placa;
            txtTipocombustivel.Text = "Tipo de combustível: " + carro.tipocombustivel;
            txtKmatual.Text = "KM atual: " + Math.Round(carro.kmatual, 3).ToString()+"KM";
            txtKmlitro.Text = "KM por litro: " + carro.kmlitro;
            txtStatus.Text = "Status: " + carro.status;
        }

        private async void btnGPS_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GPS(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnRelatorios_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Relatorios(user, carro, ""));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void btnEditarVeiculo_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarVeiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void btnExcluir_ClickedAsync(object sender, EventArgs e)
        {
            var escolha = await DisplayActionSheet("Excluir veículo?", "Sim", "Não");
            if (escolha.Equals("Sim"))
            {
                CarroController controller = new CarroController();
                //Exclui o carro e todos os relatórios dele
                controller.DeleteCarro(carro);

                CarroDataService ds = new CarroDataService();
                bool b;
                //Espera o carro ser excluido
                do
                {
                    b = false;
                    foreach (Models.Carro c in await ds.GetCarroAsync())
                    {
                        if (carro.placa.Trim() == c.placa.Trim())
                        {
                            b = true;
                            break;
                        }
                    }
                } while (b);

                await Navigation.PushAsync(new GerenciarVeiculos(user));
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GerenciarVeiculos(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}