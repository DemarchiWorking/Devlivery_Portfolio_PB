using Devlivery.Model.Domain.DAO;
using Devlivery.Model.Domain.Requisicao;
using Devlivery.Model.Domain.Resposta;

namespace Devlivery.Aplicacao.Service.Interfaces
{
    public interface IServicoService
    {
        Task<Resposta<Servico>> CadastrarServico(CadastroServicoModel servicoDto);
        Task<Resposta<Servico>> ObterCatalogoServicos(string negocioId);
    }
}
