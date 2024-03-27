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
        Task<Resposta<Negocio>> ObterCatalogoNegocioService();

        

    }
}
