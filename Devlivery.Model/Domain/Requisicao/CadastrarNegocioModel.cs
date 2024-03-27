using Devlivery.Model.Domain.Auth;
using Devlivery.Model.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.Requisicao
{
    public class CadastroNegocioModel
    {
        public string negocioId { get; set; }
        public string nome { get; set; }
        public string setor { get; set; }
        public string descricao { get; set; }
        public string link { get; set; }
        public DateTime criacao { get; set; }
        public string usuarioId { get; set; }
        public Jwt jwt { get; set; }
    }

}
