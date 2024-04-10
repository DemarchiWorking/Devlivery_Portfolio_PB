using Devlivery.Aplicacao.Service;
using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Devlivery.API.Controllers
{
        [Route("api/amizade")]
        public class AmizadeController : ControllerBase
        {
            private readonly IAmizadeService _amizadeService;
            private readonly IConfiguration _config;
            private readonly SignInManager<Usuario> _gerenciadorAcesso;
            public AmizadeController(
                IAmizadeService amizadeService,
                IConfiguration config,
                SignInManager<Usuario> gerenciadorAcesso)
            {
                _amizadeService = amizadeService;
                _config = config;
                _gerenciadorAcesso = gerenciadorAcesso;

            }
        [HttpPost("cadastrar-negocio")]
        public async Task<IActionResult> CadastrarNegocio([FromBody] ConviteAmizadeModel convite)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _amizadeService.EnviarConvite(convite);

                if (resultado.Sucesso == true)
                {
                    return Ok(resultado);
                }
                else
                {//resultado
                    return BadRequest(resultado);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return BadRequest();
        }

        [HttpGet("obter-catalogo-negocios")]
        public async Task<IActionResult> ObterCatalogoNegocio(string idConvite, bool resposta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var infoUsuarioLogado = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine(infoUsuarioLogado);
            var resultado = await _amizadeService.ResponderConvite(idConvite, resposta);

            if (resultado.ToString() != "Succeeded")
            {
                // Você pode adicionar outras lógicas aqui, como enviar um email de confirmação.
                return Ok(resultado.Dados);
            }
            return BadRequest();
        }
    }
}
   

