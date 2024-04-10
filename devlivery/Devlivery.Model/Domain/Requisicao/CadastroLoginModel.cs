using Devlivery.Model.Domain.Auth;
using Devlivery.Model.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.Requisicao
{
    public class CadastroProjetoModel
    {
        public string projetoId { get; set; }
        public string titulo { get; set; }
        public string objetivo { get; set; }
        public string descricao { get; set; }
        public string foto { get; set; }
        public float valor { get; set; }
        public string link { get; set; }
        public string usuarioId { get; set; }
        public Jwt jwt { get; set; }
    }

}