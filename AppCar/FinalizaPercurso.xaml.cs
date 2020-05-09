using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizaPercurso : ContentPage
    {
        string user;
        Models.Carro carro;

        public FinalizaPercurso(string login, Models.Carro carro, Models.Relatorio relatorio)
        {
            this.carro = carro;
            user = login;
            InitializeComponent();
            txtKmpercorridos.Text = "KM percorridos: "+ Math.Round(relatorio.kmpercorridos, 3).ToString()+"KM";
            txtCusto.Text = "R$"+relatorio.custo.ToString("F");
            txtModelo.Text = carro.modelo;
            txtKmatual.Text = "KM Atual: " + Math.Round(carro.kmatual, 3).ToString() + "KM";
            txtPlaca.Text = "Placa: " + carro.placa;
            txtStatus.Text = "Status:" + carro.status;
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Veiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}