using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.DAO
{
    public class Servico
    {
        public Guid ServicoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Contato { get; set; }
        public Guid NegocioId { get; set; }
        public virtual Negocio Negocio { get; set; }
    }
}