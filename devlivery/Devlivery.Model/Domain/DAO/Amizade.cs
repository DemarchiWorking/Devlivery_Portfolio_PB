using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.DAO
{
    public class Amizade
    {
        public Guid AmizadeId { get; set; }
        public string UsuarioEnviouId { get; set; }
        public string UsuarioRecebeuId { get; set; }
        public bool Status { get; set; }
        public virtual Usuario UsuarioEnviou { get; set; }
        public virtual Usuario UsuarioRecebeu { get; set; }

    }
}
