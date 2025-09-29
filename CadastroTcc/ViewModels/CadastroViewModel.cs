using CadastroTcc.Models;
using CadastroTcc.Services.UsuarioService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadastroTcc.ViewModels
{
    public class CadastroViewModel : BaseViewModel
    {
        private UsuarioService _usuarioService;
        public ICommand RegistrarCommand { get; set; }
        private UsuarioService _uService;
        public CadastroViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }
        public void InicializarCommands()
        {
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
        }


        #region AtributosPropriedades
        private string telefone = string.Empty;
        private string senha = string.Empty;
        public string Telefone
        {
            get => telefone;
            set
            {
                telefone = value;
                OnPropertyChanged();
            }
        }
        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Metodos
        public async Task RegistrarUsuario()    
        {
            try
            {
                Usuario u = new Usuario();
                u.Telefone = telefone;
                u.Senha = senha;

                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0)
                {
                    string mensagem = $"Usuário Id {uRegistrado.Id} registrado com sucesso.";
                    await Application.Current.MainPage.DisplayAlert("Informação", mensagem, "Ok");

                    //await Application.Current.MainPage
                       // .Navigation.PopAsync();
                       await Application.Current.MainPage
                        .Navigation.PushAsync(new Views.LoginView());
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public async Task DirecionarParaCadastro()  
        {
            try
            {
                await Application.Current.MainPage.
                    Navigation.PushAsync(new Views.LoginView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        #endregion
    }
}
