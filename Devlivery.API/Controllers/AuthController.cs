using Devlivery.Aplicacao.Service;
using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
/*
 *        using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SeuProjeto.Models; // Certifique-se de importar o namespace correto para o seu modelo de usuário
using Microsoft.IdentityModel.Tokens;
 * */
namespace Devlivery.API.Controllers
{
    [Route("api/identidade")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        //private readonly SignInManager<Usuario> _gerenciadorAcesso;
        //private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly IConfiguration _config;

            public AuthController(
                IUsuarioService usuarioService,
                IConfiguration config)
            {
                _usuarioService = usuarioService;
                _config = config;
                //_gerenciadorUsuario = gerenciadorUsuario;
                //_gerenciadorAcesso = gerenciadorAcesso;
        }

            [HttpPost("cadastrar-usuario")]
            public async Task<IActionResult> CadastrarUsuario(RegistroUsuarioModel usuarioRegistro)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Usuario usuario = new Usuario()
                {
                    Id = Guid.NewGuid().ToString(),
                    UsuarioId = Guid.NewGuid(),
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

                var result = await _usuarioService.CadastrarUsuario(usuario);
                
                if (result.ToString() != "Succeeded")
                {
                    EfetuarLoginModel efetuarLoginModel = new EfetuarLoginModel()
                    {
                         Email = usuarioRegistro.Email,
                         Senha = usuarioRegistro.Senha
                    };
                        // Você pode adicionar outras lógicas aqui, como enviar um email de confirmação.
                    return Ok(_usuarioService.GerarToken(efetuarLoginModel));
                }

                //foreach (var error in result.Errors){ModelState.AddModelError(string.Empty, error.Description);}

                return BadRequest(ModelState);
            }

            [HttpPost("fazer-login")]
            public async Task<IActionResult> Login(EfetuarLoginModel usuarioLogin)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _usuarioService.Login(usuarioLogin);
                //await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);
                //var result = _usuarioService.CadastrarUsuario(usuario);

                if (result?.Sucesso == true)
                {
                    // Você pode retornar informações adicionais, como o token JWT.
                    return Ok();
                }

                //ModelState.AddModelError(string.Empty, "Falha ao fazer login. Verifique suas credenciais.");
                return BadRequest(ModelState);
            }

        // Outros métodos, como logout, redefinição de senha, etc., podem ser adicionados aqui.
        //[Authorize]

        //private async Task<string> GerarToken(string email)
        //{}




    }
    }