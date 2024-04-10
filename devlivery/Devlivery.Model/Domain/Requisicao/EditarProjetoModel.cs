using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.Requisicao
{
    public class EditarProjetoModel
    {
        public string Titulo { get; set; }
        public string Objetivo { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public float Valor { get; set; }
        public string Link { get; set; }
        public string UsuarioId { get; set; }
    }
}
