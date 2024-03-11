using Devlivery.Model.Domain.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.DAO
{
    public class Projeto
    {
        public Guid ProjetoId { get; set; }
        public string Titulo { get; set; }
        public string Objetivo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public string Link { get; set; }
        public DateTime Criacao { get; set; }
        public float Valor { get; set; }
        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

