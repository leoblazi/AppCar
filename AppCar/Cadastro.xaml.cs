using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCar {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage {
        public Cadastro() {
            InitializeComponent();
        }

        private async void BtnVoltarCadastro_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new MainPage());
        }

        private async void BtnFinalizarCadastro_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new MainPage());
        }
    }
}