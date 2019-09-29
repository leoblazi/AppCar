using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarVeiculo : ContentPage {
        public EditarVeiculo() {
            InitializeComponent();
        }

        private async void btnVoltarClicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Veiculo());
        }
    }
}