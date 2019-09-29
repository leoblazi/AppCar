using System;

namespace W1.Models {
    public class AdicionarVeiculo {

        public string Placa { get; set; }
        public string Modelo { get; set; }
        public Double Kml { get; set; }

        public AdicionarVeiculo() {
        }

        public AdicionarVeiculo(string placa, string modelo, double kml) {
            Placa = placa;
            Modelo = modelo;
            Kml = kml;
        }
    }
}