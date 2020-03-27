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
    public partial class Cadastro : ContentPage
    {
        CadastroDataService ds;
        CadastroController controller;
        public Cadastro()
        {
            InitializeComponent();
            ds = new CadastroDataService();
            controller = new CadastroController();
        }

        private async void BtnVoltarCadastro_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnFinalizarCadastro_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                List<Models.Cadastro> cadastros = await ds.GetCadastroAsync(); //Lista com todos os cadastros
                string result; //Mensagem a ser exibida
                
                //Recebe informações
                Models.Cadastro cadastro = new Models.Cadastro
                {
                    login = txtCadLogin.Text.Trim(),
                    senha = txtCadSenha.Text.Trim(),
                    nome = txtCadNome.Text.Trim()
                };
                string confsenha = txtConfSenha.Text.Trim();

                result = controller.Cadastro(cadastro, confsenha, cadastros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                await DisplayAlert(msg[0], msg[1], msg[2]);

                if (msg[0].Equals("Sucesso"))
                {
                    await ds.AddCadastroAsync(cadastro);
                    await Navigation.PushAsync(new MainPage());
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
        }
    }
}