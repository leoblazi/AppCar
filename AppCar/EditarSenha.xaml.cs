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
        public EditarSenha(Models.Cadastro cadastro)
        {
            InitializeComponent();
            ds = new CadastroDataService();
            controller = new CadastroController();
            this.cadastro = cadastro;
        }

        private async void Salvar_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                string confSenha;
                string result; //Mensagem a ser exibida

                //Recebe informações
                Models.Cadastro novoCadastro = new Models.Cadastro
                {
                    id = cadastro.id,
                    login = cadastro.login.Trim(),
                    senha = txtNovaSenha.Text.Trim(),
                    nome = cadastro.nome.Trim()
                };
                confSenha = txtConfSenha.Text.Trim();

                result = controller.AlterarSenha(novoCadastro, cadastro, confSenha);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso"))
                {
                    await ds.UpdateCadastroAsync(novoCadastro);
                    await Navigation.PushAsync(new EditarCadastro(novoCadastro.login));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
        }

        private async void Voltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarCadastro(cadastro.login.Trim()));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}