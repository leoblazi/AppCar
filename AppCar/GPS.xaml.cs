using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.ExternalMaps;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using AppCar.Controllers;
using AppCar.Service;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GPS : ContentPage
    {
        double latitudeInicial, longitudeInicial, latitudeFinal, longitudeFinal;
        DateTime tempoInicio;
        int combustivelUtilizado;
        bool iniciado;
        string user;
        Models.Carro carro;
        Models.Relatorio relatorio;
        RelatorioController relController;
        CarroDataService dsCarro;
        CombustivelController combController;
        CombustivelDataService dsCombustivel;

        public GPS(string login, Models.Carro carro)
        {
            latitudeFinal = 0;
            longitudeFinal = 0;
            latitudeInicial = 0;
            longitudeInicial = 0;
            this.carro = carro;
            user = login;
            iniciado = false;
            relController = new RelatorioController();
            dsCarro = new CarroDataService();
            combController = new CombustivelController();
            dsCombustivel = new CombustivelDataService();
            InitializeComponent();
            txtModelo.Text = carro.modelo;
            txtKmatual.Text = "KM Atual: " + carro.kmatual.ToString("F") + "KM";
            txtPlaca.Text = "Placa: " + carro.placa;
            txtStatus.Text = "Status:" + carro.status;
        }

        private async void btnIniciarFinalizar(object sender, EventArgs e)
        {
            if (!iniciado)
            {
                iniciado = true;
                btnIniciarFinalizarPercurso.Text = "Finalizar Percurso";
                btnIniciarFinalizarPercurso.BackgroundColor = Color.Red;

                carro.status = "Em movimento";
                await dsCarro.UpdateCarroAsync(carro);
                txtStatus.Text = "Status:" + carro.status;
                txtStatus.TextColor = Color.Red;
                tempoInicio = DateTime.Now;
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 10));
                latitudeInicial = position.Latitude;
                longitudeInicial = position.Longitude;
                try
                {
                    CrossExternalMaps.Current.NavigateTo("", latitudeFinal, longitudeFinal);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Erro ao abrir o mapa.", "OK");
                }
            }
            else
            {
                try
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 10));
                    latitudeFinal = position.Latitude;
                    longitudeFinal = position.Longitude;

                    Location locationInic = new Location(latitudeInicial, longitudeInicial);
                    Location locationFinal = new Location(latitudeFinal, longitudeFinal);
                    var distanciaPercorrida = Location.CalculateDistance(locationInic, locationFinal, DistanceUnits.Kilometers);

                    carro.status = "Parado";
                    carro.kmatual += float.Parse((distanciaPercorrida).ToString());
                    await dsCarro.UpdateCarroAsync(carro);

                    relatorio = new Models.Relatorio
                    {
                        carro = carro.placa.Trim(),
                        endinicial = txtEndinic.Text.Trim(),
                        endfinal = txtEndfinal.Text.Trim(),
                        datainicial = tempoInicio,
                        datafinal = DateTime.Now,
                        kmpercorridos = float.Parse(distanciaPercorrida.ToString()),
                        custo = 0
                    };

                    List<Models.Combustivel> combustiveis = await dsCombustivel.GetCombustivelAsync();
                    Models.Combustivel combustivel = combController.GetCombustivelByCadastro(combustiveis, user);

                    relController.AddRelatorio(relatorio, combustivel, carro, combustivelUtilizado);

                    await Navigation.PushAsync(new FinalizaPercurso(user, carro, relatorio));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro:", "Preencha todos os campos.", "OK");
                }
            }
        }

        private void pckCombustivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            combustivelUtilizado = pckCombustivel.SelectedIndex;
        }

        private async void btnVoltar_ClickedAsync(object sender, EventArgs e)
        {
            carro.status = "Parado";
            await dsCarro.UpdateCarroAsync(carro);

            await Navigation.PushAsync(new Veiculo(user, carro));
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}