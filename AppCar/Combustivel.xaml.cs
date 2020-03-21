using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Combustivel : ContentPage {
        string user;
        public Combustivel(string login) {
            user = login;
            InitializeComponent();
        }

        private async void btnAlteraValor_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new AlterarValorCombustivel());
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e) {
            await Navigation.PushAsync(new Inicial(user));
        }
    }
}