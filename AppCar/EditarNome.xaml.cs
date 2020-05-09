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
    public partial class EditarNome : ContentPage
    {
        CadastroDataService ds;
        CadastroController controller;
        Models.Cadastro cadastro;
        List<Models.Cadastro> cadastros;
        public EditarNome(string login)
        {
            controller = new CadastroController();
            ds = new CadastroDataService();
            SetCadastros(login);
        }
        private async void SetCadastros(string login)
        {
            cadastros = await ds.GetCadastroAsync();
            cadastro = controller.GetCadastro(login, cadastros);
            InitializeComponent();
            txtNome.Text = cadastro.nome.Trim();
        }

        private async void Salvar_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                //Recebe informações
                Models.Cadastro novoCadastro = new Models.Cadastro
                {
                    id = cadastro.id,
                    login = cadastro.login.Trim(),
                    senha = cadastro.senha.Trim(),
                    nome = txtNome.Text.Trim(),
                    email = cadastro.email.Trim(),
                    cpf = cadastro.cpf.Trim()
                };

                if (novoCadastro.nome.Equals(""))
                    await DisplayAlert("Erro:", "Nome vazio", "OK");
                else
                {
                    await DisplayAlert("Sucesso:", "Nome alterado com sucesso!", "OK");
                    await ds.UpdateCadastroAsync(novoCadastro);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Nome vazio", "OK");
            }
        }

        private void Voltar_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
    }
}