using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service
{
    public class UsuarioServiceCaller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceCaller(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            return _usuarioService.BuscarPorEmail(email);
        }
    }
}
