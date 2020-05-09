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
    public partial class Lembretes : ContentPage {
        string user;
        List<Models.Lembrete> lembretes;
        LembreteController controller;
        LembreteDataService ds;
        public Lembretes(string login) {
            user = login;
            ds = new LembreteDataService();
            controller = new LembreteController();
            CarregaLembretes();
        }
        private async void CarregaLembretes()
        {
            lembretes = controller.GetLembretes(user, await ds.GetLembreteAsync()); //Recebe os lembretes do usuário
            InitializeComponent();
            if (lembretes.Count > 0)
                listaLembretes.ItemsSource = lembretes.OrderBy(lembretes => lembretes.lembrete).ToList(); //Manda os dados para a tela
            else
                txtTitulo.Text = "Nenhum lembrete cadastrado";
        }

        private async void Editar_ClickedAsync(object sender, EventArgs e)
        {
            var btn = ((Button)sender); //Recebe o botão selecionado
            Models.Lembrete lembrete = (Models.Lembrete)btn.CommandParameter; //Recebe o lembrete correspondente ao botão
            await Navigation.PushAsync(new EditarLembrete(user, lembrete));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void btnCriarLembrete(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CriarLembrete(user));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
    }
}