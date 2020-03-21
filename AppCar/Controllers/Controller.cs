using System;
using System.Collections.Generic;
using System.Text;

using AppCar.Service;

namespace AppCar.Controllers
{
    class Controller
    {
        public string Cadastro(Models.Cadastro cadastro, string confsenha, List<Models.Cadastro> cadastros)
        {
            //Verifica se há campos vazios
            if (cadastro.login.Equals("") || cadastro.senha.Equals("") || cadastro.nome.Equals("") || confsenha.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica se as senhas digitadas coincidem
            if (!cadastro.senha.Equals(confsenha))
                return "Erro;As senhas digitadas não coincidem;OK";

            //Verifica se o usuário já está cadastrado
            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].login.Trim().Equals(cadastro.login))
                    return "Erro;Login já cadastrado;OK";
            }
            return "Sucesso;Perfil cadastrado com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }

        public string Login(string login, string senha, List<Models.Cadastro> cadastros)
        {
            //Verifica se há campos vazios
            if (login.Equals("") || senha.Equals(""))
            {
                return "Erro;Preencha todos os campos;OK";
            }
            //Verifica login e senha
            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].login.Trim().Equals(login))
                {
                    if (cadastros[i].senha.Trim().Equals(senha))
                    {
                        return "Bem-Vindo(a) "+cadastros[i].nome.Trim()+"!;;OK";
                    }
                    return "Erro;Senha incorreta;OK";
                }
            }
            return "Erro;Usuário não cadastrado;OK";
        }

        public string CadastrarVeiculo(Models.Carro carro, List<Models.Carro> carros)
        {
            //Verifica se há campos vazios
            if (carro.placa.Equals("") || carro.modelo.Equals("") || carro.tipocombustivel.Equals("") || carro.kmatual.Equals("") || carro.kmlitro.Equals(""))
            {
                return "Erro;Preencha todos os campos;OK";
            }
            //Verifica se o carro já está cadastrado
            for (int i = 0; i < carros.Count; i++)
            {
                if (carros[i].placa.Trim().Equals(carro.placa))
                    return "Erro;Carro já cadastrado;OK";
            }
            //Verifica se Km rodados é negativo
            if (Convert.ToInt32(carro.kmatual)<0)
            {
                return "Erro;\"Km(s) rodados\" não pode ser negativo;OK";
            }
            //Verifica se Km por litro é negativo
            if (Convert.ToInt32(carro.kmlitro) < 0)
            {
                return "Erro;\"Km(s) por litro\" não pode ser negativo;OK";
            }
            return "Sucesso;Carro cadastrado com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }
    }
}
