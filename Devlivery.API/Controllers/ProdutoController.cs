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
    [Route("api/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IConfiguration _config;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;

        public ProdutoController(
        IProdutoService produtoService,
        IConfiguration config,
        SignInManager<Usuario> gerenciadorAcesso)
        {
            _produtoService = produtoService;
            _config = config;
            _gerenciadorAcesso = gerenciadorAcesso;
        }
        [HttpPost("cadastrar-produto")]
        public async Task<IActionResult> CadastrarProduto([FromBody]CadastroProdutoModel novoProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _produtoService.CadastrarProduto(novoProduto);

                if (resultado.ToString() != "Succeeded")
                {
                    return Ok(resultado);
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
        [HttpGet("obter-catalogo-produtos")]
        public async Task<IActionResult> ObterCatalogoProdutos(string negocioId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var resultado = await _produtoService.ObterCatalogoProduto(negocioId);

                if (resultado.ToString() != "Succeeded")
                {
                    // Você pode adicionar outras lógicas aqui, como enviar um email de confirmação.
                    return Ok(resultado.Dados);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}

