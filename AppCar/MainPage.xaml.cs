using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppCar;

namespace AppCar {
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void BtnCadastrar_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Cadastro());
        }

        private async void BtnLogin_ClickedAsync(object sender, EventArgs e) {
            await Navigation.PushAsync(new Inicial());
        }
    }
}