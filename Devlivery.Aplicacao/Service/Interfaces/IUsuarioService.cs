using Devlivery.Model.Domain.Auth;
using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<Resposta<string>> CadastrarUsuario(Usuario usuario);
        Task<Resposta<string>> Login(EfetuarLoginModel login);
        Task AdicionarClaim(Usuario user, string type, string value);
        Task<IdentityUser> ObetrUsuarioPorId(string userId);
        Task<IEnumerable<Usuario>> ObterTodos();
        //IdentityUser BuscarP>orEmail(string email);
        UsuarioToken GerarToken(EfetuarLoginModel login);
        Task<Resposta<string>> ObterUsuarioPorEmailService(string email);


        Usuario BuscarPorEmail(string email);
        ClaimsPrincipal ValidateToken(string token);

    }
}
