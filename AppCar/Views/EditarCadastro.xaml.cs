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
    public partial class EditarCadastro : ContentPage
    {
        string user;
        public EditarCadastro(string login)
        {
            user = login;
            InitializeComponent();
        }

        private async void AlterarLogin_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarLogin(user));
        }

        private async void AlterarSenha_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarSenha(user));
        }
        private async void AlterarNome_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarNome(user));
        }

        private void Voltar_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
    }
}