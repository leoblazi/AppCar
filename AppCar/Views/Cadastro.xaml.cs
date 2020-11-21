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

        private void BtnVoltarCadastro_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }

        private async void BtnFinalizarCadastro_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                List<Models.Cadastro> cadastros = await ds.GetCadastroAsync(); //Lista com todos os cadastros
                try
                {
                    string result; //Mensagem a ser exibida

                    //Recebe informações
                    Models.Cadastro cadastro = new Models.Cadastro
                    {
                        login = txtCadLogin.Text.Trim(),
                        senha = txtCadSenha.Text.Trim(),
                        nome = txtCadNome.Text.Trim(),
                        email = txtCadEmail.Text.Trim(),
                        cpf = txtCadCpf.Text.Trim()
                    };
                    string confsenha = txtConfSenha.Text.Trim();

                    result = controller.Cadastro(cadastro, confsenha, cadastros);

                    var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                    await DisplayAlert(msg[0], msg[1], msg[2]);

                    if (msg[0].Equals("Sucesso"))
                    {
                        await ds.AddCadastroAsync(cadastro);
                        //Adiciona os lembretes padrão ao criar um novo perfil
                        LembreteController lc = new LembreteController();
                        lc.CriarLembretes(cadastro);

                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Sem conexão com a internet", "OK");
            }
        }
    }
}