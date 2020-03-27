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
        List<Models.Cadastro> cadastros;
        Models.Cadastro cadastro;
        CadastroDataService ds;
        CadastroController controller;
        public EditarCadastro(string login)
        {
            ds = new CadastroDataService();
            controller = new CadastroController();
            SetCadastros(login);
        }

        private async void SetCadastros(string login)
        {
            cadastros =  await ds.GetCadastroAsync();
            cadastro = controller.GetCadastro(login, cadastros);
            InitializeComponent();
            ltitulo.Text = cadastro.nome.Trim() + ", nesta tela você pode editar os seus dados";
        }

        private async void AlterarLogin_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarLogin(cadastro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void AlterarSenha_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarSenha(cadastro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
        private async void AlterarNome_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarNome(cadastro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void Voltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicial(cadastro.login.Trim()));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}