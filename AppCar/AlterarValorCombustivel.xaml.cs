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
    public partial class AlterarValorCombustivel : ContentPage
    {
        string user;
        public AlterarValorCombustivel() {
            InitializeComponent();
        }

        private async void btnAtualizarValores_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Combustivel(user));
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Combustivel(user));
        }
    }
}