using Devlivery.Model.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.Requisicao
{
    public class AlterarPerfilModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Titulo { get; set; }
        public string Senha { get; set; }
        public string Foto { get; set; }
    }
}
