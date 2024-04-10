using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.DAO
{
    public class Proposta
    {
        public Guid PropostaId { get; set; }
        public string Titulo { get; set; }
        public CategoriaProposta Categoria { get; set; }

        public string Descricao { get; set; }
        public float Valor { get; set; }
        public bool Status { get; set; }
        public string UsuarioEnviaId { get; set; }
        public string UsuarioRecebeId { get; set; }
        public virtual Usuario UsuarioEnvia { get; set; }
        public virtual Usuario UsuarioRecebe { get; set; }

    }

}
