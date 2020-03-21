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
        string user;
        public GerenciarVeiculos(string login)
        {
            user = login;
            InitializeComponent();
        }

        public async void btnVeiculo_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Veiculo());
        }

        public async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicial(user));
        }
    }
}