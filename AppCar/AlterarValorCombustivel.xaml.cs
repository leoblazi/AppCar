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
    public partial class AlterarValorCombustivel : ContentPage
    {
        CombustivelDataService ds;
        Models.Combustivel combustivel;
        public AlterarValorCombustivel(Models.Combustivel combustivel)
        {
            ds = new CombustivelDataService();
            this.combustivel = combustivel;
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
                Models.Combustivel novoCombustivel = new Models.Combustivel
                {
                    id = combustivel.id,
                    login = combustivel.login.Trim(),
                    diesel = float.Parse(txtDiesel.Text.Trim()),
                    etanol = float.Parse(txtEtanol.Text.Trim()),
                    gasolina = float.Parse(txtGasolina.Text.Trim()),
                    outro = float.Parse(txtOutro.Text.Trim())
                };

                if (combustivel.etanol < 0 || combustivel.diesel < 0 || combustivel.gasolina < 0 || combustivel.outro < 0)
                    await DisplayAlert("Erro", "O valor de um combustível não pode ser negativo", "OK");
                else
                {
                    Console.WriteLine(novoCombustivel.id+novoCombustivel.login+novoCombustivel.diesel+novoCombustivel.etanol+novoCombustivel.gasolina+novoCombustivel.outro);
                    await DisplayAlert("Sucesso","Valores dos combustíveis alterados com sucesso!","OK");
                    await ds.UpdateCombustivelAsync(novoCombustivel);
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
        }
    }
}