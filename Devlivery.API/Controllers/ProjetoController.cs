using Devlivery.Aplicacao.Service;
using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Devlivery.API.Controllers
{
    [Route("api/projeto")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IConfiguration _config;

        public ProjetoController(
            IProjetoService projetoService,
            IConfiguration config)
        {
            _projetoService = projetoService;
            _config = config;
            //_gerenciadorUsuario = gerenciadorUsuario;
            //_gerenciadorAcesso = gerenciadorAcesso;
        }

        [HttpPost("cadastrar-projeto")]
        public async Task<IActionResult> CadastrarProjeto(CadastroProjetoModel projeto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _projetoService.CadastrarProjeto(projeto);

            if (result.ToString() != "Succeeded")
            {
                // Você pode adicionar outras lógicas aqui, como enviar um email de confirmação.
                return Ok();
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("Edt")]
        public string EditarProjeto()
        {

            EditarProjetoModel projetoEd = new EditarProjetoModel();
            if (!ModelState.IsValid)
                return null;// BadRequest(ModelState);

            var result = _projetoService.EditarProjeto(projetoEd);

            return null;
        }

        [Authorize]
        [HttpPost("Test")]
        public string test(string projeto)
        {
            return null;
        }
    }
}