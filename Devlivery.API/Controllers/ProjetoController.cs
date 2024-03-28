using Devlivery.Aplicacao.Service;
using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace Devlivery.API.Controllers
{
    [Route("api/projeto")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IConfiguration _config;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        public ProjetoController(
            IProjetoService projetoService,
            IConfiguration config,
            SignInManager<Usuario> gerenciadorAcesso)
        {
            _projetoService = projetoService;
            _config = config;
            _gerenciadorAcesso = gerenciadorAcesso;
        //_gerenciadorUsuario = gerenciadorUsuario;
        //_gerenciadorAcesso = gerenciadorAcesso;
    }
        [HttpPost("cadastrar-projeto")]
        [Authorize]
        public async Task<IActionResult> CadastrarProjeto([FromBody] CadastroProjetoModel projeto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _projetoService.CadastrarProjeto(projeto, "");

                if (resultado.Sucesso == true)
                {
                    List<dynamic> dados = new List<dynamic>();
                    foreach (PropertyInfo propriedade in projeto.GetType().GetProperties())
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
            catch(Exception ex)
            {
               Console.WriteLine(ex.ToString());
            }
            return BadRequest();

        }

        [HttpGet("obter-catalogo-projetos")]
        public async Task<IActionResult> ObterCatalogoProjetos()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var infoUsuarioLogado = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Console.WriteLine(infoUsuarioLogado);
            var resultado = await _projetoService.ObterCatalogoService();

            if (resultado.Sucesso == true)
            {
                // Você pode adicionar outras lógicas aqui, como enviar um email de confirmação.
                return Ok(resultado.Dados);
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

        [HttpGet("obter-projetosssss")]
        [Authorize]
        public string ObterCatalogoProjetosssss()
        {
            // async Task<IActionResult>[FromBody] CadastroProjetoModel projeto
            return "Test";

        }
    }
}