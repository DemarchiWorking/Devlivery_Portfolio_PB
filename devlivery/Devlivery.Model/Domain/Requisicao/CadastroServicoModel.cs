using Devlivery.Model.Domain.Auth;
using Devlivery.Model.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.Requisicao
{
    public class CadastroServicoModel
    {
        public string ServicoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Contato { get; set; }
        public string NegocioId { get; set; }
        public string UsuarioId { get; set; }
        public Jwt Jwt { get; set; }
    }
}
