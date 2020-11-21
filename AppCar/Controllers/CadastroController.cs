using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AppCar.Service;

namespace AppCar.Controllers
{
    class CadastroController
    {
        public string Cadastro(Models.Cadastro cadastro, string confsenha, List<Models.Cadastro> cadastros)
        {
            //Verifica se há campos vazios
            if (cadastro.login.Equals("") || cadastro.senha.Equals("") || cadastro.nome.Equals("") || cadastro.email.Equals("") || cadastro.cpf.Equals("") || confsenha.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica se as senhas digitadas coincidem
            if (!cadastro.senha.Equals(confsenha))
                return "Erro;As senhas digitadas não coincidem;OK";

            //Verifica se a cpf contém todos os caracteres
            if (cadastro.cpf.Trim().Length < 11)
                return "Erro;O CPF deve conter todos os caracteres;OK";

            //Verifica se o login ou cpf já está cadastrado
            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].login.Trim().Equals(cadastro.login))
                    return "Erro;Login já cadastrado;OK";
                if (cadastros[i].cpf.Trim().Equals(cadastro.cpf))
                    return "Erro;CPF já cadastrado;OK";
            }
            return "Sucesso;Perfil cadastrado com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }

        public string AlterarLogin(Models.Cadastro novoCadastro, Models.Cadastro cadastro, List<Models.Cadastro> cadastros)
        {
            //Verifica se há campos vazios
            if (novoCadastro.login.Equals("") || novoCadastro.senha.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica se a senha está correta
            if (!novoCadastro.senha.Equals(cadastro.senha.Trim()))
                return "Erro;Senha incorreta;OK";

            if (!novoCadastro.login.Equals(cadastro.login))
            {
                //Verifica se o login já está cadastrado
                for (int i = 0; i < cadastros.Count; i++)
                {
                    if (cadastros[i].login.Trim().Equals(novoCadastro.login))
                        return "Erro;Login já cadastrado;OK";
                }
            }
            UpdateLogin(novoCadastro, cadastro, cadastros);

            return "Sucesso;Login alterado com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }
        private async void UpdateLogin(Models.Cadastro novoCadastro, Models.Cadastro cadastro, List<Models.Cadastro> cadastros)
        {
            CadastroDataService ds = new CadastroDataService();
            if (!novoCadastro.login.Equals(cadastro.login.Trim())) //Se o login foi alterado
            {
                novoCadastro.cpf = cadastro.cpf;
                cadastro.cpf = "";
                await ds.UpdateCadastroAsync(cadastro);
                await ds.AddCadastroAsync(novoCadastro); //Adciona o cadastro com o login alterado

                CarroDataService carrods = new CarroDataService();
                CombustivelDataService combds = new CombustivelDataService();
                LembreteDataService lemds = new LembreteDataService();
                List<Models.Carro> carros = await carrods.GetCarroAsync();
                List<Models.Combustivel> combustiveis = await combds.GetCombustivelAsync();
                List<Models.Lembrete> lembretes = await lemds.GetLembreteAsync();

                for (int i = 0; i < carros.Count; i++)
                {
                    if (carros[i].dono.Trim().Equals(cadastro.login.Trim())) //Para todo carro com o login alterado, muda para o novo login
                    {
                        Models.Carro carro = new Models.Carro
                        {
                            id = carros[i].id,
                            placa = carros[i].placa.Trim(),
                            dono = novoCadastro.login,
                            modelo = carros[i].modelo.Trim(),
                            tipocombustivel = carros[i].tipocombustivel.Trim(),
                            kmatual = carros[i].kmatual,
                            kmlitro = carros[i].kmlitro,
                            status = carros[i].status.Trim()
                        };
                        await carrods.UpdateCarroAsync(carro);
                    }
                }
                for (int i = 0; i < combustiveis.Count; i++)
                {
                    if (combustiveis[i].login.Trim().Equals(cadastro.login.Trim())) //Para cada combustivel com o login, muda para o login alterado
                    {
                        Models.Combustivel combustivel = combustiveis[i];
                        combustivel.login = novoCadastro.login;
                        await combds.UpdateCombustivelAsync(combustivel);
                    }
                }
                for (int i = 0; i < lembretes.Count; i++)
                {
                    if (lembretes[i].login.Trim().Equals(cadastro.login.Trim())) //Para cada lembrete com o login, muda para o login alterado
                    {
                        Models.Lembrete lembrete = lembretes[i];
                        lembrete.login = novoCadastro.login;
                        await lemds.UpdateLembreteAsync(lembrete);
                    }
                }
                await ds.DeleteCadastroAsync(cadastro);
            }
        }

        public string AlterarSenha(Models.Cadastro novoCadastro, Models.Cadastro cadastro, string confSenha)
        {
            //Verifica se há campos vazios
            if (novoCadastro.senha.Equals("") || confSenha.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica se a senha está correta
            if (!cadastro.senha.Trim().Equals(confSenha))
                return "Erro;Senha incorreta;OK";

            return "Sucesso;Senha alterada com sucesso!;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }

        public string Login(string login, string senha, List<Models.Cadastro> cadastros) //Controle da validação da tela MainPage na entrada do login
        {
            //Verifica se há campos vazios
            if (login.Equals("") || senha.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica login e senha
            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].login.Trim().Equals(login))
                {
                    if (cadastros[i].senha.Trim().Equals(senha))
                    {
                        return "Bem-Vindo(a) " + cadastros[i].nome.Trim() + "!;;OK";
                    }
                    return "Erro;Senha incorreta;OK";
                }
            }
            return "Erro;Usuário não cadastrado;OK";
        }

        public Models.Cadastro GetCadastro(string login, List<Models.Cadastro> cadastros)
        {
            Models.Cadastro cadastro = new Models.Cadastro();

            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].login.Trim().Equals(login))
                    cadastro = cadastros[i];
            }
            return cadastro;
        } //Faz a varredura e atualiza as informações como nome, senha e login

        public string RecuperarLogin(string email, string cpf, List<Models.Cadastro> cadastros)
        {
            //Verifica se há campos vazios
            if (email.Equals("") || cpf.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica email e cpf
            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].cpf.Trim().Equals(cpf) && cadastros[i].email.Trim().Equals(email))
                    return cadastros[i].login+";;";
            }
            return "Erro;Dados incorretos;OK";
        }

        public string RedefinirSenha(string login, string senha, string confsenha, List<Models.Cadastro> cadastros)
        {
            //Verifica se há campos vazios
            if (senha.Equals("") || confsenha.Equals(""))
                return "Erro;Preencha todos os campos;OK";

            //Verifica se as senhas digitadas coincidem
            if (!senha.Equals(confsenha))
                return "Erro;As senhas digitadas não coincidem;OK";

            //Altera a senha
            for (int i = 0; i < cadastros.Count; i++)
            {
                if (cadastros[i].login.Trim().Equals(login))
                {
                    MudaSenha(cadastros[i], senha);
                    return "Sucesso;Senha redefinida com sucesso!;OK";
                }
            }
            return "Erro;Perfil não encontrado;OK"; //Se passar por todas as verificações, retorna uma mensagem de "sucesso"
        }

        public async void MudaSenha(Models.Cadastro cadastro, string senha)
        {
            CadastroDataService ds = new CadastroDataService();
            cadastro.senha = senha;
            await ds.UpdateCadastroAsync(cadastro);
        }
    }
}
