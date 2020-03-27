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
    public partial class EditarLogin : ContentPage
    {
        CadastroDataService ds;
        CadastroController controller;
        Models.Cadastro cadastro;
        public EditarLogin(Models.Cadastro cadastro)
        {
            InitializeComponent();
            ds = new CadastroDataService();
            controller = new CadastroController();
            txtLogin.Text = cadastro.login.Trim();
            this.cadastro = cadastro;
        }

        private async void Salvar_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                List<Models.Cadastro> cadastros = await ds.GetCadastroAsync(); //Lista com todos os cadastros
                string result; //Mensagem a ser exibida

                //Recebe informações
                Models.Cadastro novoCadastro = new Models.Cadastro
                {
                    id = cadastro.id,
                    login = txtLogin.Text.Trim(),
                    senha = txtSenha.Text.Trim(),
                    nome = cadastro.nome.Trim()
                };

                result = controller.AlterarLogin(novoCadastro, cadastro, cadastros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso"))
                {
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