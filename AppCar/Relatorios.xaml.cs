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
    public partial class Relatorios : ContentPage
    {
        RelatorioDataService ds;
        RelatorioController controller;
        List<Models.Relatorio> relatorios;
        Models.Carro carro;
        string user, filtro;
        public Relatorios(string login, Models.Carro carro, string filtro)
        {
            ds = new RelatorioDataService();
            controller = new RelatorioController();
            this.carro = carro;
            user = login;
            this.filtro = filtro;
            CarregaRelatorios();
        }
        private async void CarregaRelatorios()
        {
            if (filtro != "")
            {
                try
                {
                    relatorios = controller.GetRelatorioByData(await ds.GetRelatorioAsync(), carro, Convert.ToDateTime(filtro).Date); //Recebe os relatórios do carro com filtro de data
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro:", "Formato de data incorreto", "OK");
                    relatorios = controller.GetRelatorioByCarro(await ds.GetRelatorioAsync(), carro); //Recebe os relatórios do carro
                }
            }
            else
                relatorios = controller.GetRelatorioByCarro(await ds.GetRelatorioAsync(), carro); //Recebe os relatórios do carro

            InitializeComponent();
            txtFiltro.Text = filtro;
            if (relatorios.Count > 0)
                listaRelatorios.ItemsSource = relatorios.OrderBy(relatorios => relatorios.datainicial).ToList(); //Exibe os relatórios na tela
            else
                txtVazio.IsVisible = true;
        }

        private async void btnFiltro_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Relatorios(user, carro, txtFiltro.Text.Trim()));
            }
            catch (Exception ex)
            {
                await Navigation.PushAsync(new Relatorios(user, carro, ""));
            }
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }


        private async void btnLimparFiltro_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Relatorios(user, carro, ""));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Veiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}