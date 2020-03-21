using System;

namespace AppCar.Models
{
    public class Combustivel
    {
        public int id { get; set; }
        public string login { get; set; }
        public float etanol { get; set; }
        public float diesel { get; set; }
        public float gasolina { get; set; }
        public float outro { get; set; }
    }
}