using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
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
        public Projeto() { }
        public Projeto(
            Guid projetoId,
            string titulo,
            string objetivo,
            string descricao,
            string foto, 
            string link, 
            float valor, 
            string usuarioId,
            DateTime criacao)
        { 
            this.ProjetoId = projetoId;
            this.Titulo = titulo;   
            this.Objetivo = objetivo;
            this.Descricao = descricao;
            this.Foto = foto;
            this.Valor = valor;
            this.Link = link;
            this.Criacao = criacao;
            this.UsuarioId = usuarioId;
        }
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

