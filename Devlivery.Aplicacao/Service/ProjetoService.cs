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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service
{        
    public class ProjetoService : IProjetoService
    {
        // adicionar usuario herdamdo identity
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        private readonly IHttpContextAccessor _accessor;
        public ProjetoService(
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
        public async Task<Resposta<dynamic>> CadastrarProjeto(CadastroProjetoModel cadastroProjetoModel, string jwt)
        {
            try
            {
                //var autoriza = _accessor.HttpContext.Request.Headers["Authorization"].ToString();
                //var zz = await this._context.Usuarios.FindAsync();
                // var email = User.FindFirst("sub")?.Value;
                //var tz = _gerenciadorUsuario.GetUserId();
                var projeto = new Projeto()
                {
                    ProjetoId = Guid.NewGuid(),
                    Titulo = cadastroProjetoModel.titulo,
                    Valor = cadastroProjetoModel.valor, 
                    Criacao = DateTime.Now,
                    Descricao = cadastroProjetoModel.descricao,
                    Foto = cadastroProjetoModel.foto,
                    Link = cadastroProjetoModel.link,
                    Objetivo = cadastroProjetoModel.objetivo,
                    UsuarioId = cadastroProjetoModel.usuarioId,
                };

                _context.Projeto.Add(projeto);
                var resposta = _context.SaveChanges();
                if (resposta == 1)
                {
                    
                    return new Resposta<dynamic>()
                    {
                            Titulo = "Usuário cadastrado com sucesso.",
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

        public async Task<Resposta<dynamic>> ObterCatalogoService()
        {
            try
            {
                //var autoriza = _accessor.HttpContext.Request.Headers["Authorization"].ToString();
                //var zz = await this._context.Usuarios.FindAsync();
                // var email = User.FindFirst("sub")?.Value;
                //var tz = _gerenciadorUsuario.GetUserId();


                var projetos = _context.Projeto;

                var query = projetos.Select(p => new Projeto()
                {
                    Titulo = p.Titulo,
                    Descricao = p.Descricao,
                    Foto = p.Foto,
                    Criacao = p.Criacao,
                    UsuarioId = p.UsuarioId,
                    Usuario = p.Usuario
                }).ToList();

                //var resposta = _context.SaveChanges();
                if (query.Count > 0)
                {

                    return new Resposta<dynamic>()
                    {
                        Titulo = "Usuário cadastrado com sucesso.",
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
        public async Task<Resposta<dynamic>> EditarProjeto(EditarProjetoModel editarProjetoModel)
        {
            try
            {
                var autoriza = _accessor.HttpContext.Request.Headers["Authorization"].ToString();
                var zz = await this._context.Usuarios.FindAsync();
                // var email = User.FindFirst("sub")?.Value;
                //var tz = _gerenciadorUsuario.GetUserId();


                var resposta = 1;// _context.SaveChanges();                _context.Projeto.Add(projeto);
                if (resposta == 1)
                {

                    return new Resposta<dynamic>()
                    {
                        Titulo = "Usuário cadastrado com sucesso.",
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
        /*
        public async Task<Resposta<dynamic>> Login(EfetuarLoginModel login)
        {
            try
            {
                //var cadastro = _gerenciadorUsuario.CreateAsync(usuario, usuario.Senha);if (cadastro.IsCompleted)}
                var autenticacao = await _gerenciadorAcesso.PasswordSignInAsync(login.Email, login.Senha, false, true);

                if (autenticacao.Succeeded)
                {
                    var usuario = await _gerenciadorUsuario.FindByNameAsync(login.Email);
                    var role = await _gerenciadorUsuario.GetRolesAsync(usuario);
                    var rolename = role.ToString();

                    if (usuario != null)
                    {
                        return new Resposta<dynamic>()
                        {
                            Titulo = "Usuario logado com sucesso",
                            Status = 200,
                            Dados = new List<dynamic>() { usuario },
                            Sucesso = true,
                        };
                    }
                    else
                    {
                        return new Resposta<dynamic>()
                        {
                            Titulo = "Usuario nãO foi identificado",
                            Status = 400,
                            Dados = new List<dynamic>() { "" },
                            Sucesso = false,
                        };

                    }
                }
                else
                {
                    return new Resposta<dynamic>() { Titulo = "ERRO" };
                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<dynamic>()
            {
                Titulo = "Erro 500",
                Status = 500,
                Dados = null,
                Sucesso = false
            };
        */

    }

    }
