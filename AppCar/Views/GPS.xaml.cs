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
using System.Threading;

namespace AppCar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GPS : ContentPage
    {
        double latitudeInicial, longitudeInicial, latitudeFinal, longitudeFinal, distancia;
        DateTime tempoInicio;
        int combustivelUtilizado;
        bool iniciado, finalizado;
        string user;
        Models.Carro carro;
        Models.Relatorio relatorio;
        RelatorioController relController;
        CarroDataService dsCarro;
        CombustivelController combController;
        CombustivelDataService dsCombustivel;

        public GPS(string login, Models.Carro carro)
        {
            distancia = 0;
            latitudeFinal = 0;
            longitudeFinal = 0;
            latitudeInicial = 0;
            longitudeInicial = 0;
            this.carro = carro;
            user = login;
            iniciado = false;
            finalizado = false;
            relController = new RelatorioController();
            dsCarro = new CarroDataService();
            combController = new CombustivelController();
            dsCombustivel = new CombustivelDataService();
            InitializeComponent();
            txtModelo.Text = carro.modelo;
            txtKmatual.Text = "KM Atual: " + Math.Round(carro.kmatual, 3).ToString() + "KM";
            txtPlaca.Text = "Placa: " + carro.placa;
            txtStatus.Text = "Status:" + carro.status;
        }

        private async void btnIniciarFinalizar(object sender, EventArgs e)
        {
            if (!iniciado)
            {
                try
                {
                    var locator = CrossGeolocator.Current; //Variavel que vai atribuir Localização atual
                    locator.DesiredAccuracy = 50; //Precisão 50mt
                    var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 10)); //Pega posição do celular
                    iniciado = true;
                    btnIniciarFinalizarPercurso.Text = "Finalizar Percurso";
                    btnIniciarFinalizarPercurso.BackgroundColor = Color.Red;
                    carro.status = "Em movimento";
                    await dsCarro.UpdateCarroAsync(carro); //atualizando o status do carro
                    txtStatus.Text = "Status:" + carro.status;
                    txtStatus.TextColor = Color.Red;
                    tempoInicio = DateTime.UtcNow;
                    latitudeInicial = position.Latitude;
                    longitudeInicial = position.Longitude;
                    CalculaDistancia();
                    await CrossExternalMaps.Current.NavigateTo("", latitudeFinal, longitudeFinal); //acessa o mapa padrão
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Ative sua Localização para continuar.", "OK");
                }
            }
            else
            {
                try
                {
                    relatorio = new Models.Relatorio
                    {
                        carro = carro.placa.Trim(),
                        endinicial = txtEndinic.Text.Trim(),
                        endfinal = txtEndfinal.Text.Trim(),
                        datainicial = tempoInicio.AddHours(-3),
                        datafinal = DateTime.UtcNow.AddHours(-3)
                    };
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro:", "Preencha todos os campos.", "OK");
                }
                try
                {
                    finalizado = true;
                    var locator = CrossGeolocator.Current; //Variavel que vai atribuir Localização atual
                    locator.DesiredAccuracy = 50; //Precisão 50mt
                    var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 10)); //Pega posição do celular
                    latitudeFinal = position.Latitude;
                    longitudeFinal = position.Longitude;

                    Location locationInic = new Location(latitudeInicial, longitudeInicial);
                    Location locationFinal = new Location(latitudeFinal, longitudeFinal);
                    var distanciaPercorrida = distancia+Location.CalculateDistance(locationInic, locationFinal, DistanceUnits.Kilometers);

                    carro.status = "Parado";
                    carro.kmatual += float.Parse((distanciaPercorrida).ToString("N3")); //3 casas decimais - Soma e iguala ao kmatual
                    await dsCarro.UpdateCarroAsync(carro); //atualiza os valores do carro no banco

                    relatorio.kmpercorridos = float.Parse(distanciaPercorrida.ToString("N3")); //insere no relatório o valores

                    List<Models.Combustivel> combustiveis = await dsCombustivel.GetCombustivelAsync(); 
                    Models.Combustivel combustivel = combController.GetCombustivelByCadastro(combustiveis, user);

                    relController.AddRelatorio(relatorio, combustivel, carro, combustivelUtilizado); //instância para calcular o custo

                    await Navigation.PushAsync(new FinalizaPercurso(user, carro, relatorio));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro:", "Ative sua Localização para continuar.", "OK");
                }
            }
        }

        private async void CalculaDistancia()
        {
            try
            {
                var locator = CrossGeolocator.Current; //Pega posição do celular
                locator.DesiredAccuracy = 30; //Precisão
                var position = await locator.GetPositionAsync(timeout: new TimeSpan(0, 0, 30)); //Atualiza de 30 em 30 segundos
                var latitude = position.Latitude;
                var longitude = position.Longitude;

                if (!finalizado)
                {
                    Location locationInic = new Location(latitudeInicial, longitudeInicial);
                    Location locationFinal = new Location(latitude, longitude);
                    distancia += Location.CalculateDistance(locationInic, locationFinal, DistanceUnits.Kilometers); //calcula a distancia entre o I e o F
                    latitudeInicial = latitude;
                    longitudeInicial = longitude;
                    txtKmatual.Text = "KM Atual: " + Math.Round((carro.kmatual+distancia), 3).ToString() + "KM"; //soma ao km atual (só coloca na tela)
                    CalculaDistancia();
                }
            }
            catch (Exception ex){}
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