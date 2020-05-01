using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppCar.Service;
using AppCar.Controllers;

namespace AppCar
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        CadastroDataService ds;
        CadastroController controller;
        public MainPage() {
            InitializeComponent();
            ds = new CadastroDataService();
            controller = new CadastroController();
        }

        private async void BtnCadastrar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnEsqueciSenha_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecuperarSenha());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnLogin_ClickedAsync(object sender, EventArgs e)
        {
            try{
                List<Models.Cadastro> cadastros = await ds.GetCadastroAsync(); //Lista com todos os cadastros
                string result; //Mensagem a ser exibida
                //Recebe os dados
                string login = txtUsuario.Text.Trim();
                string senha = txtSenha.Text.Trim();

                result = controller.Login(login, senha, cadastros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                if (!msg[0].Equals("Erro"))
                {
                    await Navigation.PushAsync(new Inicial(login));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await DisplayAlert(msg[0], msg[1], msg[2]);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Campos vazios", "OK");
            }
        }
    }
}