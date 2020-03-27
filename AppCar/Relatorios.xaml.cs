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
        string user;
        public Relatorios(string login, Models.Carro carro)
        {
            ds = new RelatorioDataService();
            controller = new RelatorioController();
            this.carro = carro;
            user = login;
            CarregaRelatorios();
        }
        private async void CarregaRelatorios()
        {
            relatorios = controller.GetRelatorioByCarro(await ds.GetRelatorioAsync(), carro); //Recebe os relatórios do carro
            InitializeComponent();
            listaRelatorios.ItemsSource = relatorios.OrderBy(relatorios => relatorios.datainicial).ToList(); //Exibe os relatórios na tela
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Veiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}