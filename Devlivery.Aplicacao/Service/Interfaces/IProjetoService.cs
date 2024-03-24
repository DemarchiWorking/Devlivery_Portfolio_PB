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
    public interface IProjetoService
    {
        Task<Resposta<dynamic>> CadastrarProjeto(CadastroProjetoModel cadastroProjetoModel, string jwt);

        Task<Resposta<dynamic>> ObterCatalogoService();


        Task<Resposta<dynamic>> EditarProjeto(EditarProjetoModel editarProjetoModel);
    }
}
