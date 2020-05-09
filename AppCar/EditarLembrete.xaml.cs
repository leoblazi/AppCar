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
    public partial class EditarLembrete : ContentPage {
        string user;
        Models.Lembrete lembrete;
        LembreteDataService ds;
        public EditarLembrete(string login, Models.Lembrete lembrete) {
            user = login;
            this.lembrete = lembrete;
            ds = new LembreteDataService();
            InitializeComponent();
            txtLembrete.Text = lembrete.lembrete.Trim();
        }

        private async void Salvar_ClickedAsync(object sender, EventArgs e)
        {
            lembrete.lembrete = txtLembrete.Text.Trim();
            await ds.UpdateLembreteAsync(lembrete);
            await DisplayAlert("Sucesso", "Lembrete alterado com sucesso!", "OK");
            await Navigation.PushAsync(new Lembretes(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void Excluir_ClickedAsync(object sender, EventArgs e)
        {
            await ds.DeleteLembreteAsync(lembrete);
            var escolha = await DisplayActionSheet("Excluir lembrete?", "Sim", "Não");
            if (escolha.Equals("Sim"))
            {
                await DisplayAlert("Sucesso", "Lembrete excluido com sucesso!", "OK");
                await Navigation.PushAsync(new Lembretes(user));
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lembretes(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}