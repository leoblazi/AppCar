﻿using System;

namespace WebAPI.Models
{
    public class Cadastro
    {
        public int id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string cpf { get; set; }
    }
}