﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppCar.Service;
using AppCar.Controllers;

namespace AppCar
{
    [DesignTimeVisible(false)]
    public partial class RecuperarSenha : ContentPage
    {
        CadastroDataService ds;
        CadastroController controller;
        public RecuperarSenha() {
            InitializeComponent();
            ds = new CadastroDataService();
            controller = new CadastroController();
        }

        private async void BtnVoltar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }

        private async void BtnConfirmar(object sender, EventArgs e)
        {
            try{
                List<Models.Cadastro> cadastros = await ds.GetCadastroAsync(); //Lista com todos os cadastros
                string result; //Mensagem a ser exibida
                //Recebe os dados
                string login = txtLogin.Text.Trim();
                string email = txtEmail.Text.Trim();
                string cpf = txtCpf.Text.Trim();

                result = controller.RecuperarSenha(login, email, cpf, cadastros);

                var msg = System.Text.RegularExpressions.Regex.Split(result, ";"); //Faz a separação da mensagem em 3 strings
                if (!msg[0].Equals("Erro"))
                {
                    await Navigation.PushAsync(new RedefinirSenha(login));
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                else
                    await DisplayAlert(msg[0], msg[1], msg[2]);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", "Campos vazios", "OK");
            }
        }
    }
}