using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GerenciarVeiculos : ContentPage {
        public GerenciarVeiculos() {
            InitializeComponent();
        }

        public async void btnVeiculo_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Veiculo());
        }

        public async void btnVoltar_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Inicial());
        }
    }
}