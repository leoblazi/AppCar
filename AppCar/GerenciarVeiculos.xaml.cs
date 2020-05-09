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
    public partial class GerenciarVeiculos : ContentPage
    {
        CarroDataService ds;
        CarroController controller;
        List<Models.Carro> carros;
        string user;
        public GerenciarVeiculos(string login)
        {
            ds = new CarroDataService();
            controller = new CarroController();
            user = login;
            CarregaCarros();
        }
        private async void CarregaCarros()
        {
            carros = controller.GetCarroByCadastro(await ds.GetCarroAsync(), user); //Recebe os carros do usuário
            InitializeComponent();
            if (carros.Count > 0)
                listaCarros.ItemsSource = carros.OrderBy(carros => carros.modelo).ToList(); //Manda os dados para a tela
            else
                txtTitulo.Text = "Nenhum carro cadastrado";
        }

        private async void Veiculo_ClickedAsync(object sender, EventArgs e)
        {
            var btn = ((Button)sender); //Recebe o botão selecionado
            Models.Carro carro = (Models.Carro)btn.CommandParameter; //Recebe o carro correspondente ao botão
            await Navigation.PushAsync(new Veiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }
    }
}