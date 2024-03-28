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
    public class AmizadeService : IAmizadeService
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        private readonly IHttpContextAccessor _accessor;

        public AmizadeService(
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
        public async Task<Resposta<Amizade>> EnviarConvite(ConviteAmizadeModel convite)
        {
            try
            {
                var usuario = await _gerenciadorAcesso.UserManager.FindByNameAsync(convite.Jwt.usuario);

                var amizade = new Amizade()
                {
                    AmizadeId = Guid.NewGuid(),
                    Status = false,
                    UsuarioEnviouId = convite.UsuarioEnviouId,
                    UsuarioRecebeuId = convite.UsuarioRecebeuId,
                };

                _context.Amizades.Add(amizade);
                var resposta = _context.SaveChanges();
                if (resposta == 1)
                {

                    return new Resposta<Amizade>()
                    {
                        Titulo = "Convite de Amizade enviado com sucesso.",
                        Dados = new List<Amizade>() { amizade },
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Amizade>()
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
            return new Resposta<Amizade>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };

        }
        public async Task<Resposta<Amizade>> ResponderConvite(string idConvite, bool resposta)
        {
            try
            {
                var amizade = _context.Amizades;
                List<Amizade> query = amizade.Select(p => new Amizade()
                {
                    AmizadeId = p.AmizadeId,
                    Status = p.Status, 
                    UsuarioEnviouId = p.UsuarioEnviouId,
                    UsuarioRecebeuId = p.UsuarioRecebeuId,
                }).ToList();

                //var resposta = _context.SaveChanges();
                if (query?.Count > 0)
                {
                    return new Resposta<Amizade>()
                    {
                        Titulo = "Convite respondido com sucesso.",
                        Dados = query,
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Amizade>()
                    {
                        Titulo = "Erro ao responder convite",
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
                return new Resposta<Amizade>()
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
