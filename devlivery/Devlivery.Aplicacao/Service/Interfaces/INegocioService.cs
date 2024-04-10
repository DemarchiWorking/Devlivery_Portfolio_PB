using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlivery.Aplicacao.Service.Interfaces
{
    public interface INegocioService
    {
        Task<Resposta<dynamic>> CadastrarNegocio(CadastroNegocioModel cadastroProjetoModel);
        Task<Resposta<dynamic>> DeletarNegocio(string negocioId, string usuario);
        Task<Resposta<Negocio>> ObterCatalogoNegocioService(string usuarioLogado);
        Task<Resposta<Negocio>> VerificarSeUsuarioPossuiNegocio(string negocio);
    }
}
