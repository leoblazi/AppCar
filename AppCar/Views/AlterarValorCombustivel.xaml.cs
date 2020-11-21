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
    public partial class AlterarValorCombustivel : ContentPage
    {
        string user;
        CombustivelDataService ds;
        Models.Combustivel combustivel;
        CombustivelController controller;
        public AlterarValorCombustivel(string login)
        {
            user = login;
            ds = new CombustivelDataService();
            controller = new CombustivelController();
            GetCombustivel();
        }

        private async void GetCombustivel()
        {
            List<Models.Combustivel> combustiveis = await ds.GetCombustivelAsync();
            combustivel = controller.GetCombustivelByCadastro(combustiveis, user);
            InitializeComponent();
            txtEtanol.Text = combustivel.etanol.ToString("F");
            txtDiesel.Text = combustivel.diesel.ToString("F");
            txtGasolina.Text = combustivel.gasolina.ToString("F");
            txtOutro.Text = combustivel.outro.ToString("F");
        }

            private async void btnAtualizarValores_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Recebe informações
                combustivel.diesel = float.Parse(txtDiesel.Text.Trim());
                combustivel.etanol = float.Parse(txtEtanol.Text.Trim());
                combustivel.gasolina = float.Parse(txtGasolina.Text.Trim());
                combustivel.outro = float.Parse(txtOutro.Text.Trim());

                if (combustivel.etanol < 0 || combustivel.diesel < 0 || combustivel.gasolina < 0 || combustivel.outro < 0)
                    await DisplayAlert("Erro", "O valor de um combustível não pode ser negativo", "OK");
                else
                {
                    await DisplayAlert("Sucesso","Valores dos combustíveis alterados com sucesso!","OK");
                    await ds.UpdateCombustivelAsync(combustivel);
                    await Navigation.PushAsync(new Combustivel(combustivel.login.Trim()));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Preencha todos os campos", "OK");
            }
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Combustivel(combustivel.login.Trim()));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}