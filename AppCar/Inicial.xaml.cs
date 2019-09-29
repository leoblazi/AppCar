using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicial : ContentPage {
        public Inicial() {
            InitializeComponent();
        }

        private async void BtnGerenciarVeiculos_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new GerenciarVeiculos());
        }

        private async void BtnAdicionar_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new AdicionarVeiculo());
        }

        private async void BtnRelatorios_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Relatorios());
        }

        private async void BtnCombustivel_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Combustivel());
        }

        private async void BtnEditar_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new EditarCadastro());
        }

        private async void BtnDicas_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Dicas());
        }

        private async void BtnSair_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new MainPage());
        }
    }
}