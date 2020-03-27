using System;

namespace AppCar.Models
{
    public class Relatorio
    {
        public int id { get; set; }
        public string carro { get; set; }
        public string endinicial { get; set; }
        public string endfinal { get; set; }
        public DateTime datainicial { get; set; }
        public DateTime datafinal { get; set; }
        public float kmpercorridos { get; set; }
        public float custo { get; set; }
    }
}