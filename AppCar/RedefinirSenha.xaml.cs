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
    public partial class RedefinirSenha : ContentPage
    {
        string login;
        CadastroDataService ds;
        CadastroController controller;
        public RedefinirSenha(string login) {
            InitializeComponent();
            this.login = login;
            ds = new CadastroDataService();
            controller = new CadastroController();
        }

        private async void BtnVoltar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecuperarSenha());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnRedefinir(object sender, EventArgs e)
        {
            try{
                List<Models.Cadastro> cadastros = await ds.GetCadastroAsync(); //Lista com todos os cadastros
                string result; //Mensagem a ser exibida
                //Recebe os dados
                string senha = txtSenha.Text.Trim();
                string confsenha = txtConfirmarSenha.Text.Trim();

                result = controller.RedefinirSenha(login, senha, confsenha, cadastros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings

                if (!msg[0].Equals("Erro"))
                {
                    await Navigation.PushAsync(new MainPage());
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