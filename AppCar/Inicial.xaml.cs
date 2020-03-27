using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCar.Models;
using AppCar.Service;
using AppCar.Controllers;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Inicial : ContentPage
    {
        string user;
        public Inicial(string login)
        {
            InitializeComponent();
            user = login;
            lBemvindo.Text = "Seja bem - vindo, "+login.Trim()+"!";
        }

        private async void BtnGerenciarVeiculos_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GerenciarVeiculos(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnAdicionar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdicionarVeiculo(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnCombustivel_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Combustivel(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnEditar_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new EditarCadastro(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnDicas_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Dicas(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnSair_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}