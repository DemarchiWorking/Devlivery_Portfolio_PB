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
    public interface IAmizadeService
    {
        Task<Resposta<Amizade>> EnviarConvite(ConviteAmizadeModel convite);
        Task<Resposta<Amizade>> ResponderConvite(string idConvite, bool resposta);

    }
}
