using System;

namespace WebAPI.Models
{
    public class Carro
    {
        public int id { get; set; }
        public string placa { get; set; }
        public string modelo { get; set; }
        public string dono { get; set; }
        public string tipocombustivel { get; set; }
        public float kmatual { get; set; }
        public float kmlitro { get; set; }
        public string status { get; set; }
    }
}