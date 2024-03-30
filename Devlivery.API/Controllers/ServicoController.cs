using Devlivery.Aplicacao.Service;
using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Devlivery.API.Controllers
{
    [Route("api/servico")]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _servicoService;
        private readonly IConfiguration _config;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        public ServicoController(
            IServicoService servicoService,
            IConfiguration config,
            SignInManager<Usuario> gerenciadorAcesso)
        {
            _servicoService = servicoService;
            _config = config;
            _gerenciadorAcesso = gerenciadorAcesso;
        }

        [HttpPost("cadastrar-servico")]
        public async Task<IActionResult> CadastrarServico([FromBody] CadastroServicoModel novoServico)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (novoServico?.Jwt!.usuario == null || novoServico?.Jwt!.usuario == "")
                {
                    return Unauthorized();
                }

                var resultado = await _servicoService.CadastrarServico(novoServico);

                if (resultado.Sucesso == true)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("obter-catalogo-servicos")]
        public async Task<IActionResult> ObterCatalogoServicos(string negocioId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var resultado = await _servicoService.ObterCatalogoServicos(negocioId);

                if (resultado.Sucesso == true)
                {
                    if (resultado.Dados.Count > 0)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return NotFound(resultado);
                    }
                }
                return BadRequest(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("obter-alunos")]
        public async Task<IActionResult> ObterAlunos()
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var resultado = await _servicoService.ObterAlunos();

                if (resultado.Sucesso == true)
                {
                    if (resultado.Dados.Count > 0)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return NotFound(resultado);
                    }
                }
                return BadRequest(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

}
