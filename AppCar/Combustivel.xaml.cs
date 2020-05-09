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
    public partial class Combustivel : ContentPage
    {
        CombustivelDataService ds;
        CombustivelController controller;
        Models.Combustivel combustivel;
        string user;
        public Combustivel(string login)
        {
            ds = new CombustivelDataService();
            controller = new CombustivelController();
            user = login;
            GetCombustivel();
        }

        private async void GetCombustivel()
        {
            List<Models.Combustivel> combustiveis = await ds.GetCombustivelAsync();
            combustivel = controller.GetCombustivelByCadastro(combustiveis, user);
            InitializeComponent();
            txtEtanol.Text = "R$" + combustivel.etanol.ToString("F");
            txtDiesel.Text = "R$" + combustivel.diesel.ToString("F");
            txtGasolina.Text = "R$" + combustivel.gasolina.ToString("F");
            txtOutro.Text = "R$" + combustivel.outro.ToString("F");
        }

        private async void btnAlteraValor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlterarValorCombustivel(combustivel));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private void btnVoltar_Clicked(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
    }
}