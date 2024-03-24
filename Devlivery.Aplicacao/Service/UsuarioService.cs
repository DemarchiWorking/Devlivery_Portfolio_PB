using Devlivery.Aplicacao.Service.Interfaces;
using Devlivery.Infraestrutura;
using Devlivery.Model.Domain.Auth;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service
{
    public class UsuarioService : IUsuarioService
    {
        // adicionar usuario herdamdo identity
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _gerenciadorUsuario;
        private readonly SignInManager<Usuario> _gerenciadorAcesso;
        private readonly RoleManager<IdentityRole> _gerenciadorRole;
        public UsuarioService(
            RoleManager<IdentityRole> gerenciadorRole,
            UserManager<Usuario> gerenciadorUsuario,
            SignInManager<Usuario> gerenciadorAcesso,
            AppDbContext context,
            IConfiguration configuration)
        {
            _gerenciadorRole = gerenciadorRole;
            _gerenciadorUsuario = gerenciadorUsuario;
            _gerenciadorAcesso = gerenciadorAcesso;
            _context = context;
            _configuration = configuration;
        }
        public async Task<Resposta<string>> CadastrarUsuario(Usuario usuario)
        {
            try
            {
                // Criação de roles (papéis)
                bool existeRoleAluno = await _gerenciadorRole.RoleExistsAsync("usuario");
                if (existeRoleAluno == false)
                {
                    await _gerenciadorRole.CreateAsync(new IdentityRole("usuario"));
                    await _gerenciadorRole.CreateAsync(new IdentityRole("aluno"));

                }

                var cadastro = await _gerenciadorUsuario.CreateAsync(usuario, usuario.Senha);
                if (usuario.Email.Contains("al.infnet"))
                { await _gerenciadorUsuario.AddToRoleAsync(usuario, "aluno"); }
                else
                {
                    await _gerenciadorUsuario.AddToRoleAsync(usuario, "usuario");//
                }
                if (cadastro!.Succeeded)
                {
                    List<string> dadoResposta = new List<string>
                    {
                        string.Join(", ", usuario)
                    };

                    return new Resposta<string>()
                    {
                        Titulo = "Usuário cadastrado com sucesso.",
                        Dados = dadoResposta,
                        Status = 200,
                        Sucesso = true
                    };
                }
                else
                {
                    var erro = " ";
                    foreach(var err in cadastro!.Errors)
                    {
                        erro += err.Code + " " + err.Description;
                    }
                   
                    return new Resposta<string>()
                    {
                        Titulo = "Erro ao cadastrar com usuario:",
                        Status = 400,
                        Sucesso = false,
                        Dados = new List<string>() { erro.ToString()
                        }
                    };
                }
            }
            catch (Exception e)
            {
                List<string> erroCatch = new List<string>()
                {
                    e.ToString(),
                    e.StackTrace!.ToString(),
                    e.Message.ToString(),
                    e.InnerException!.ToString(),
                    e.Source!.ToString(),
                    string.Join(" ", e!.GetObjectData)
            };
                return new Resposta<string>()
                {
                    Titulo = "Erro 500",
                    Dados = erroCatch,
                    Status = 500,
                    Sucesso = false
                };
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
           
        }
        public async Task<Resposta<string>> Login(EfetuarLoginModel login)
        {
            try
            {
                //var cadastro = _gerenciadorUsuario.CreateAsync(usuario, usuario.Senha);if (cadastro.IsCompleted)}
                var autenticacao = await _gerenciadorAcesso.PasswordSignInAsync(login.Email, login.Senha, false, true);

                if (autenticacao.Succeeded)
                {
                    var usuario = await _gerenciadorUsuario.FindByNameAsync(login.Email);
                    var role = await _gerenciadorUsuario.GetRolesAsync(usuario);
                    var rolename = role.ToString();

                    if (usuario != null)
                    {
                        return new Resposta<string>()
                        {
                            Titulo = "Usuario logado com sucesso",
                            Status = 200,
                            Dados = new List<string>()
                            { usuario.Id,
                              usuario.Titulo,
                              usuario.UserName,
                              usuario.Email,
                              usuario.Foto,
                              usuario.Telefone
                            },
                            Sucesso = true,
                        };
                    }
                    else
                    {
                        return new Resposta<string>()
                        {
                            Titulo = "Usuario nãO foi identificado",
                            Status = 204,
                            Dados = new List<string>() { "" },
                            Sucesso = false,
                        };

                    }
                }
                else
                {
                    return new Resposta<string>() { Titulo = autenticacao.ToString() };
                }
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return new Resposta<string>()
            {
                Titulo = "Erro 500",
                Status = 500,
                Dados = null,
                Sucesso = false
            };            
        }

        public async Task<Resposta<string>> ObterUsuarioPorEmailService(string email)
        {
            try
            {
                //var cadastro = _gerenciadorUsuario.CreateAsync(usuario, usuario.Senha);if (cadastro.IsCompleted)}
                var resposta = _context.Usuarios.FirstOrDefault(x => x.Email == email);

                if (resposta != null)
                {
                    var usuario = new List<string>()
                    {
                        resposta.Email,
                        resposta.Foto,
                        resposta.Telefone,
                        resposta.Nome,
                        resposta.Titulo,
                        resposta.PhoneNumber,
                    };

                    return new Resposta<string>()
                    {
                        Titulo = "Usuario encontrado com sucesso.",
                        Status = 200,
                        Dados = usuario,
                        Sucesso = true
                    };
                }
                else
                {
                    return new Resposta<string>()
                    {
                        Titulo = "Erro ao encontrar usuario.",
                        Status = 204,
                        Dados = null,
                        Sucesso = false
                    };
                }

            }
            catch (Exception ex)
            {

            }
            return new Resposta<string>()
            {
                Titulo = "[ERRO:500] Ao tentar encontrar usuario.",
                Status = 400,
                Dados = null,
                Sucesso = false
            };
        }


            //demarchialteracao alterar Usuario por userresponmsedto
            public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            var usuarios = new List<Usuario>();
            foreach (var user in _gerenciadorUsuario.Users)
            {
                var userDto = new Usuario
                {
                    //UserId = user.Id,
                    Email = user.Email,
                    //EmailConfirmado = user.EmailConfirmed
                };
                usuarios.Add(userDto);
            }
            return await Task.FromResult(usuarios);
        }

        public async Task AdicionarClaim(Usuario user, string type, string value)
        {
            await _gerenciadorUsuario.AddClaimAsync(user, new Claim(type, value));
        }
        public Usuario BuscarPorEmail(string email)
        {
            try
            {
                Usuario resp = new Usuario();
                resp = _context?.Usuarios?.FirstOrDefault(x => x.Email == email);

                return resp;
            }
            catch (Exception e)
            {
                //_logger.Error(e, $"[ListarServicos] Fatal error on ListarServicos");
            }
            return null;
        }
        public async Task<IdentityUser> ObetrUsuarioPorId(string userId)
        {
            return await Task.FromResult(_gerenciadorUsuario.Users.FirstOrDefault(u => u.Id == userId));
        }
        //public async Task<string> GerarToken(string email){}

        public UsuarioToken GerarToken(EfetuarLoginModel login)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, login.Email),
                new Claim("role","Usuario"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            return new UsuarioToken()
            {
                Autenticado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracao = expiration,
                Mensagem = "Token JWT: Sucesso.",
                Usuario = login.Email
            };
        }

        private async Task<ClaimsIdentity> ObterClaimUsuario(ICollection<Claim> claims, Usuario usuario)
        {
            var userRoles = await _gerenciadorUsuario.GetRolesAsync(usuario);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }
        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetConnectionString("Secret"));
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetConnectionString("Issuer"),
                Audience = _configuration.GetConnectionString("Audience"),
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration.GetConnectionString("ExpiracaoEmHoras"))),
                // SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
        private long ToUnixEpochDate(DateTime date)
        {
            return 0;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }

}
/*

            /*
            var user = await _gerenciadorAcesso.FindByEmailAsync(email);
            var claims = await _gerenciadorUsuario.GetClaimsAsync(user);

            var identityClaims = await ObterClaimUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);
            return encodedToken;

if (query == null)
{
    //if(query.Count == 0)


}
if (query.Count() == 0)
{
    return new Resposta<dynamic>()
    {
        Titulo = "Usuário ou senha inválido.",
        Status = 204,
        Dados = null,
        Sucesso = true
    };
}
else
{
    var resposta = query.FirstOrDefault();
    if (resposta != null) resposta.Senha = "****";

    return new Resposta<dynamic>()
    {
        Titulo = "Usuário encontrados com sucesso.",
        Status = 200,
        Dados = new List<dynamic>() { resposta },
        Sucesso = true
    };
}
}*/// (.Id).FirstOrDefault();
/*var query = _context.Usuarios
    .AsNoTracking()
    .Where(x => x.Email == login.Email && x.Senha == login.Senha)
    .FirstOrDefault();
 * _context.Usuarios.Where(x => (x.Email == login.Email) && (x.Senha == login.Senha)).ToList();
 * solicitacao.Select(p => new Usuario()
{
    UsuarioId = p.UsuarioId,
    Nome = p.Nome,
    Email = p.Email,
    PhoneNumber = p.PhoneNumber,
    UserName = p.UserName,
    Foto = string.Empty,
})
    .Where(x => (x.Email == login.Email) && (x.Senha == login.Senha))
    .ToList();*/