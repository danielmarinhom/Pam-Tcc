using CadastroTcc.Services.UsuarioService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CadastroTcc.ViewModels
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService _usuarioService;
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistarCommand { get; set; }
    }
}
