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
        public FinalizaPercurso()
        {
            InitializeComponent();
        }

        private async void btnVoltar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Inicial(user));
        }
    }
}