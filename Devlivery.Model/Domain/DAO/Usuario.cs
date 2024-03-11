using Devlivery.Model.Domain.DAO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.DAO
{
    public class Usuario : IdentityUser
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Titulo { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public DateTime Criacao { get; set; }


        public virtual ICollection<Negocio> Negocios { get; set; }
        public virtual ICollection<Projeto> Projetos { get; set; }
        public virtual ICollection<Amizade> AmizadesEnviadas { get; set; }
        public virtual ICollection<Amizade> AmizadesRecebidas { get; set; }
        public virtual ICollection<Proposta> PropostasEnviadas { get; set; }
        public virtual ICollection<Proposta> PropostasRecebidas { get; set; }
    }
}
