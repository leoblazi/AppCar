using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCar.Service;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarNome : ContentPage
    {
        CadastroDataService ds;
        Models.Cadastro cadastro;
        public EditarNome(Models.Cadastro cadastro)
        {
            InitializeComponent();
            ds = new CadastroDataService();
            txtNome.Text = cadastro.nome.Trim();
            this.cadastro = cadastro;
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
                    nome = txtNome.Text.Trim()
                };

                if (novoCadastro.nome.Equals(""))
                    await DisplayAlert("Erro:", "Nome vazio", "OK");
                else
                {
                    await DisplayAlert("Sucesso:", "Nome alterado com sucesso!", "OK");
                    await ds.UpdateCadastroAsync(novoCadastro);
                    await Navigation.PushAsync(new EditarCadastro(novoCadastro.login));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Nome vazio", "OK");
            }
        }

        private async void Voltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarCadastro(cadastro.login.Trim()));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}