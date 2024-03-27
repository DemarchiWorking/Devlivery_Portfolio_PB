using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Devlivery.API.Controllers
{
    [Route("api/identidade")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _config;

            public AuthController(
                IUsuarioService usuarioService,
                IConfiguration config)
            {
                _usuarioService = usuarioService;
                _config = config;
        }
            
            [HttpPost("cadastrar-usuario")]
            [Authorize]
            public async Task<IActionResult> CadastrarUsuario([FromBody] RegistroUsuarioModel usuarioRegistro)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Usuario usuario = new Usuario()
                {
                    UserName = usuarioRegistro.Email,
                    Nome = usuarioRegistro.Nome,
                    Email = usuarioRegistro.Email,
                    Senha = usuarioRegistro.Senha,
                    Telefone = usuarioRegistro.Telefone,
                    PhoneNumber = usuarioRegistro.Telefone,
                    Titulo = "Iniciante",
                    Foto = usuarioRegistro.Senha,
                    Criacao = DateTime.Now,
                    PasswordHash = Guid.NewGuid().ToString()                    
                };

                var resultado = await _usuarioService.CadastrarUsuario(usuario);
                
                if (resultado.Sucesso == true)
                {
                    EfetuarLoginModel efetuarLoginModel = new EfetuarLoginModel()
                    {
                         Email = usuarioRegistro.Email,
                         Senha = usuarioRegistro.Senha
                    };

                    return Ok(_usuarioService.GerarToken(efetuarLoginModel));
                }
                else
                {
                    return BadRequest(resultado);
                }
   
                //foreach (var error in result.Errors){ModelState.AddModelError(string.Empty, error.Description);}

            }

            [HttpPost("fazer-login")]
            public async Task<IActionResult> Login([FromBody] EfetuarLoginModel efetuarLogin)
            {

                var resultado = await _usuarioService.Login(efetuarLogin);
                //await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);
                //var result = _usuarioService.CadastrarUsuario(usuario);

                
                if (resultado?.Sucesso == true)
                {
                    EfetuarLoginModel efetuarLoginModel = new EfetuarLoginModel()
                    {
                        Email = efetuarLogin.Email,
                        Senha = efetuarLogin.Senha
                    };

                    return Ok(_usuarioService.GerarToken(efetuarLoginModel));
                }
                return BadRequest(resultado);
            }


        [HttpPost("obter-usuario-por-email")]
        public async Task<IActionResult> ObterUsuarioPorEmail(string email)
        {
            try
            {
                var resultado = await _usuarioService.ObterUsuarioPorEmailService(email);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
            }


            return null;
        }


                // Outros métodos, como logout, redefinição de senha, etc., podem ser adicionados aqui.
                //[Authorize]

                //private async Task<string> GerarToken(string email)
                //{}




        }
    }