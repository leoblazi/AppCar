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
    public partial class EditarSenha : ContentPage
    {
        CadastroDataService ds;
        CadastroController controller;
        Models.Cadastro cadastro;
        List<Models.Cadastro> cadastros;
        public EditarSenha(string login)
        {
            ds = new CadastroDataService();
            controller = new CadastroController();
            SetCadastros(login);
        }
        private async void SetCadastros(string login)
        {
            cadastros = await ds.GetCadastroAsync();
            cadastro = controller.GetCadastro(login, cadastros);
            InitializeComponent();
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
                    senha = txtNovaSenha.Text.Trim(),
                    nome = cadastro.nome.Trim(),
                    email = cadastro.email.Trim(),
                    cpf = cadastro.cpf.Trim()
                };
                string confSenha = txtConfSenha.Text.Trim();

                string result; //Mensagem a ser exibida
                result = controller.AlterarSenha(novoCadastro, cadastro, confSenha);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso"))
                {
                    await ds.UpdateCadastroAsync(novoCadastro);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
        }

        private void Voltar_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
    }
}