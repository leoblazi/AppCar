using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdicionarVeiculo : ContentPage {
        public AdicionarVeiculo() {
            InitializeComponent();
        }

        private async void btnVoltarAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Inicial());
        }

        private async void btnCadastrarVeiculoAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Inicial());
        }
    }
}