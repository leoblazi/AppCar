using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Veiculo : ContentPage {
        public Veiculo() {
            InitializeComponent();
        }

        private async void btnGPS_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new GPS());
        }

        private async void btnEditarVeiculo_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new EditarVeiculo());
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new GerenciarVeiculos());
        }
    }
}