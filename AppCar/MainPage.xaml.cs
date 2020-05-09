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
            Limpar();
        }

        private async void BtnEsqueci_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecuperarLogin());
            Limpar();
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
                    LembreteDataService ds = new LembreteDataService();
                    //Carrega todos os lembretes
                    List<Models.Lembrete> lembretes = await ds.GetLembreteAsync();

                    LembreteController lc = new LembreteController();
                    //Retorna um lembrete aleatório, se houver
                    string lembrete = lc.GetLembrete(login, lembretes);

                    await Navigation.PushAsync(new Inicial(login));
                    Limpar();
                    await DisplayAlert(msg[0], lembrete, msg[2]); //Exibe um lembrete aleatório ao usuário, além da mensagem padrão
                }
                else
                    await DisplayAlert(msg[0], msg[1], msg[2]);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", ex.Message, "OK");
            }
        }

        private void Limpar()
        {
            txtUsuario.Text = "";
            txtSenha.Text = "";
        }
    }
}