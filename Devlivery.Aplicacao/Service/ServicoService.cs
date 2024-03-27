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
    public class ServicoService : IServicoService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        private readonly IHttpContextAccessor _accessor;

        public ServicoService(
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
        public async Task<Resposta<Servico>> CadastrarServico(CadastroServicoModel servicoDto)
        {
            try
            {
                var usuario = await _gerenciadorAcesso.UserManager.FindByNameAsync(servicoDto.Jwt.usuario);

    
                servicoDto.UsuarioId = usuario.Id;

                var servico = new Servico()
                {
                    ServicoId = Guid.NewGuid(),
                    Nome = servicoDto.Nome,
                    Descricao = servicoDto.Descricao,
                    NegocioId = Guid.Parse(servicoDto.NegocioId),
                    Contato = servicoDto.Contato,
                };

                _context.Servicos.Add(servico);
                var resposta = _context.SaveChanges();
                if (resposta == 1)
                {

                    return new Resposta<Servico>()
                    {
                        Titulo = "Servico cadastrado com sucesso.",
                        Dados = new List<Servico>() { servico },
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Servico>()
                    {
                        Titulo = "Erro ao cadastrar com Serviço",
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
            return new Resposta<Servico>()
            {
                Titulo = "Erro 500.",
                Dados = null,
                Status = 500,
                Sucesso = false
            };
        }
        public async Task<Resposta<Servico>> ObterCatalogoServicos(string negocioId)
        {
            try
            {
                var servicos = _context.Servicos;
                List<Servico> query = servicos.Select(p => new Servico()
                {
                    ServicoId = Guid.NewGuid(),
                    Nome = p.Nome,
                    Descricao = p.Descricao,
                    NegocioId = Guid.Parse(negocioId),
                    Contato = p.Contato
                }).ToList();

                if (query?.Count > 0)
                {
                    return new Resposta<Servico>()
                    {
                        Titulo = "Lista de servicos encontrada com sucesso.",
                        Dados = query,
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<Servico>()
                    {
                        Titulo = $"Não foi encontrada os serviços do negócio {negocioId}",
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
                return new Resposta<Servico>()
                {
                    Titulo = "[ObterCatalogoServicos]Erro 500 " + e.Message.ToString() + e.StackTrace.ToString() + e.InnerException.ToString() + e.Source.ToString() + e.GetObjectData,
                    Dados = null,
                    Status = 500,
                    Sucesso = false
                };
            }
        }

    }
}


