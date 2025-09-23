using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroTcc.Models;

namespace CadastroTcc.Services.UsuarioService
{
    public class UsuarioService : Request
    {
        private readonly Request _request;
        private const string _apiUrlBase = "https://rpgapi3ai2025.azurewebsites.net/Usuarios";
        public UsuarioService()
        {
            _request = new Request();
        }
        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";
            u.Id = await _request.PostReturnIntAsync(_apiUrlBase + urlComplementar, u, string.Empty);
            return u;
        }
    }
}
