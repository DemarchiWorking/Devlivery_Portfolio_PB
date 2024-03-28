using Devlivery.Model.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.Requisicao
{
    public class ConviteAmizadeModel
    {
            public Guid AmizadeId { get; set; }
            public string UsuarioEnviouId { get; set; }
            public string UsuarioRecebeuId { get; set; }
            public bool Status { get; set; }
            public Jwt Jwt { get; set; }


    }
}
