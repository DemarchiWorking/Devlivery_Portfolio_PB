using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devlivery.Model.Domain.DAO
{
    public class Negocio
    {
        public Guid NegocioId { get; set; }
        public string Nome { get; set; }
        public string Setor { get; set; }
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
        public ICollection<Servico> Servicos { get; set; }
        public string Link { get; set; }
        public DateTime Criacao { get; set; }
        public string UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

    }
}

/*dotnet ef migrations add InitialCreate
dotnet ef database update

Adicionando Serviços e Produtos:
Para adicionar um Serviço, você pode fazer algo como:
C#

var negocio = _context.Negocios.Find(negocioId); // Encontre o Negócio desejado
var servico = new Servico
{
    Nome = "Serviço de Manutenção",
    Descricao = "Realiza manutenção preventiva em equipamentos.",
    Contato = "contato@empresa.com"
};
negocio.Servicos.Add(servico);
_context.SaveChanges();
AI-generated code. Review and use carefully. More info on FAQ.
Para adicionar um Produto, faça algo semelhante:
C#

var negocio = _context.Negocios.Find(negocioId); // Encontre o Negócio desejado
var produto = new Produto
{
    Nome = "Computador",
    Descricao = "Computador de alta performance",
    Valor = 2500.00f
};
negocio.Produtos.Add(produto);
_context.SaveChanges();
AI-generated code. Review and use carefully. More info on FAQ.
Consultando os Dados:
Você pode consultar os serviços e produtos associados a um negócio da seguinte maneira:
C#

var negocio = _context.Negocios.Include(n => n.Servicos).Include(n => n.Produtos).FirstOrDefault(n => n.NegocioId == negocioId);
var servicos = negocio.Servicos;
var produtos = negocio.Produtos;

*/
