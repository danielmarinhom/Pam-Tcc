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
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistarCommand { get; set; }
        public void InicializarCommands()
        {

            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
        }
        public async Task<Usuario> AutenticarUsuario()
        {

        }
    }
}
