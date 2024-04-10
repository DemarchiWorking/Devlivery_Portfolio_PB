using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Infraestrutura;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service
{
    public class NegocioService : INegocioService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        private readonly UsuarioServiceCaller _usuarioServiceCaller;
        private readonly IHttpContextAccessor _accessor;
        public NegocioService(
            AppDbContext context,
            IConfiguration configuration,
            RoleManager<IdentityRole> gerenciadorRole,
            UserManager<Usuario> gerenciadorUsuario,
            SignInManager<Usuario> gerenciadorAcesso,
            UsuarioServiceCaller usuarioServiceCaller,
            IHttpContextAccessor accessor)
        {
            _context = context;
            _configuration = configuration;
            _gerenciadorUsuario = gerenciadorUsuario;
            _gerenciadorAcesso = gerenciadorAcesso;
            _usuarioServiceCaller = usuarioServiceCaller;
            _accessor = accessor;
        }
        public async Task<Resposta<dynamic>> CadastrarNegocio(CadastroNegocioModel cadastroProjetoModel)
        {
            try
            {
                var usuario = await _gerenciadorAcesso.UserManager.FindByNameAsync(cadastroProjetoModel.jwt.usuario);
                cadastroProjetoModel.usuarioId = usuario.Id;

                var negocio = new Negocio()
                {
                    NegocioId = Guid.NewGuid(),
                    Nome = cadastroProjetoModel.nome,
                    Setor = cadastroProjetoModel.setor,
                    Criacao = DateTime.Now,
                    Descricao = cadastroProjetoModel.descricao,
                    Link = cadastroProjetoModel.link,
                    UsuarioId = cadastroProjetoModel.usuarioId,
                    Produtos = null,
                    Servicos = null,
                };

                _context.Negocios.Add(negocio);
                var resposta = _context.SaveChanges();
                if (resposta == 1)
                {

                    return new Resposta<dynamic>()
                    {
                        Titulo = "Negócio cadastrado com sucesso.",
                        Dados = new List<dynamic>() { "OK" },
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Erro ao cadastrar com usuario",
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
            return new Resposta<dynamic>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }

        public async Task<Resposta<dynamic>> DeletarNegocio(string negocioId, string usuario)
        {
            try
            {
                var negocio = _context.Negocios.Find(Guid.Parse(negocioId));

                if (negocio == null)
                {
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Não existe serviço com esse Id",
                        Dados = new List<dynamic>() { negocio },
                        Status = 204,
                        Sucesso = true
                    };
                }

                _context.Negocios.Remove(negocio);
                var resposta = _context.SaveChanges();
                if (resposta == 1)
                {
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Negócio cadastrado com sucesso.",
                        Dados = new List<dynamic>() { "OK" },
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<dynamic>()
                    {
                        Titulo = "Erro ao cadastrar com usuario",
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
            return new Resposta<dynamic>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }

        public async Task<Resposta<Negocio>> ObterCatalogoNegocioService(string usuarioLogado)
        {
            try
            {
                var negocios = _context.Negocios;
                Console.WriteLine(usuarioLogado);

                List<Negocio> query = negocios.Select(p => new Negocio()
                {
                    Descricao = p.Descricao,
                    Criacao = p.Criacao,
                    Usuario = p.Usuario,
                    Link = p.Link,
                    NegocioId = p.NegocioId,
                    Nome = p.Nome,
                    Produtos = p.Produtos,
                    Servicos = p.Servicos,
                    Setor = p.Setor, 
                    UsuarioId = p.UsuarioId
                }).ToList();
                //var resposta = _context.SaveChanges();
                if (query?.Count > 0)
                {

                    return new Resposta<Negocio>()
                    {
                        Titulo = "Usuário cadastrado com sucesso.",
                        Dados = query,
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Negocio>()
                    {
                        Titulo = "Erro ao cadastrar com usuario",
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
                return new Resposta<Negocio>()
                {
                    Titulo = "[ObterCatalogoNegocioService]Erro 500 " + e.Message.ToString() + e.StackTrace.ToString() + e.InnerException.ToString() + e.Source.ToString() + e.GetObjectData,
                    Dados = null,
                    Status = 500,
                    Sucesso = false
                };
            }
        }


        public async Task<Resposta<Negocio>> VerificarSeUsuarioPossuiNegocio(string usuarioEmail)
        {
            try
            {
                var usuario = _usuarioServiceCaller.BuscarUsuarioPorEmail(usuarioEmail);
                var negocios = _context.Negocios;

                List<Negocio> query = negocios
                    .Where(p => p.UsuarioId == usuario.Id) 
                    .Select(p => new Negocio()
                {
                    Descricao = p.Descricao,
                    Criacao = p.Criacao,
                    Usuario = p.Usuario,
                    Link = p.Link,
                    NegocioId = p.NegocioId,
                    Nome = p.Nome,
                    Produtos = p.Produtos,
                    Servicos = p.Servicos,
                    Setor = p.Setor,
                    UsuarioId = p.UsuarioId
                }).ToList();
                //var resposta = _context.SaveChanges();
                if (query?.Count > 0)
                {
                    return new Resposta<Negocio>()
                    {
                        Titulo = "Usuário cadastrado com sucesso.",
                        Dados = query,
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Negocio>()
                    {
                        Titulo = "Erro ao cadastrar com usuario",
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
                return new Resposta<Negocio>()
                {
                    Titulo = "[ObterCatalogoNegocioService]Erro 500 " + e.Message.ToString() + e.StackTrace.ToString() + e.InnerException.ToString() + e.Source.ToString() + e.GetObjectData,
                    Dados = null,
                    Status = 500,
                    Sucesso = false
                };
            }
        }

}


}