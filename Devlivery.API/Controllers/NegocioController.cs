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
    [Route("api/negocio")]
    public class NegocioController : Controller
    {
        private readonly INegocioService _negocioService;
        private readonly IConfiguration _config;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;

        public NegocioController(
        INegocioService negocioService,
        IConfiguration config,
        SignInManager<Usuario> gerenciadorAcesso)
        {
            _negocioService = negocioService;
            _config = config;
            _gerenciadorAcesso = gerenciadorAcesso;
        }

        [HttpPost("cadastrar-negocio")]
        [Authorize]
        public async Task<IActionResult> CadastrarNegocio([FromBody] CadastroNegocioModel novoNegocio)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _negocioService.CadastrarNegocio(novoNegocio);

                if (resultado.ToString() != "Succeeded")
                {
                    List<dynamic> dados = new List<dynamic>();
                    foreach (PropertyInfo propriedade in novoNegocio.GetType().GetProperties())
                    {
                        dados.Add(propriedade);
                    }
                    if (resultado!.Dados.Count > 0)
                    {
                        foreach (var proj in resultado.Dados)
                        {
                            dados.Add(proj);
                        }
                    }
                    resultado.Dados = dados;
                    return Ok();
                }
                else
                {//resultado
                    return BadRequest();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return BadRequest();
        }

        [HttpGet("obter-catalogo-negocios")]
        public async Task<IActionResult> ObterCatalogoNegocio()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var infoUsuarioLogado = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine(infoUsuarioLogado);
            var resultado = await _negocioService.ObterCatalogoNegocioService();

            if (resultado.ToString() != "Succeeded")
            {
                // Você pode adicionar outras lógicas aqui, como enviar um email de confirmação.
                return Ok(resultado.Dados);
            }
            return BadRequest();
        }
    }
}


