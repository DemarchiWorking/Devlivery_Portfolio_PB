using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Infraestrutura;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        private readonly IHttpContextAccessor _accessor;

        public ProdutoService(
                AppDbContext context,
                IConfiguration configuration,
                RoleManager<IdentityRole> gerenciadorRole,
                UserManager<Usuario> gerenciadorUsuario,
                SignInManager<Usuario> gerenciadorAcesso,
                IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = configuration;
            _gerenciadorUsuario = gerenciadorUsuario;
            _gerenciadorAcesso = gerenciadorAcesso;
            _accessor = accessor;
        }
        public async Task<Resposta<Produto>> CadastrarProduto(CadastroProdutoModel produtoDto)
        {
            try
            {
                var usuario = await _gerenciadorAcesso.UserManager.FindByNameAsync(produtoDto.Jwt.usuario);
                produtoDto.UsuarioId = usuario.Id;

                var produto = new Produto()
                {
                    ProdutoId = Guid.NewGuid(),
                    Nome = produtoDto.Nome,
                    Descricao = produtoDto.Descricao,
                    NegocioId = Guid.Parse(produtoDto.NegocioId),
                    Valor = produtoDto.Valor, 
                    Negocio = null
                };

                _context.Produtos.Add(produto);
                var resposta = _context.SaveChanges();
                if (resposta == 1)
                {

                    return new Resposta<Produto>()
                    {
                        Titulo = "Produto cadastrado com sucesso.",
                        Dados = new List<Produto>() { produto },
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Produto>()
                    {
                        Titulo = "Erro ao cadastrar com Produto",
                        Status = 400,
                        Sucesso = false
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.GetObjectData);
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<Produto>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }
        public async Task<Resposta<Produto>> ObterCatalogoProduto(string negocioId)
        {
            try
            {
                var produtos = _context.Produtos;
                List<Produto> query = produtos.Select(p => new Produto()
                {
                    ProdutoId = p.ProdutoId,
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    NegocioId = p.NegocioId,
                    Valor = p.Valor
                }).ToList();

                //var resposta = _context.SaveChanges();
                if (query?.Count > 0)
                {
                    return new Resposta<Produto>()
                    {
                        Titulo = "Produto cadastrado com sucesso.",
                        Dados = query,
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Produto>()
                    {
                        Titulo = "Erro ao cadastrar com produto",
                        Status = 400,
                        Sucesso = false,
                        Dados = null
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.GetObjectData);
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
                return new Resposta<Produto>()
                {
                    Titulo = "[ObterCatalogoProdutos]Erro 500 " + e.Message.ToString() + e.StackTrace.ToString() + e.InnerException.ToString() + e.Source.ToString() + e.GetObjectData,
                    Dados = null,
                    Status = 500,
                    Sucesso = false
                };
            }
        }
    }
}


