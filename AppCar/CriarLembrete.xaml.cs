using AppCar.Controllers;
using AppCar.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CriarLembrete : ContentPage {
        string user;
        LembreteDataService ds;
        public CriarLembrete(string login) {
            user = login;
            ds = new LembreteDataService();
            InitializeComponent();
        }

        private async void Salvar_ClickedAsync(object sender, EventArgs e)
        {
            Models.Lembrete lembrete = new Models.Lembrete
            {
                login = user,
                lembrete = txtLembrete.Text.Trim()
            };
            await ds.AddLembreteAsync(lembrete);
            await DisplayAlert("Sucesso", "Lembrete adicionado com sucesso!", "OK");
            await Navigation.PushAsync(new Lembretes(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lembretes(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}