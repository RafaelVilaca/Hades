using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface IVotacaoAppService
    {
        HttpResponseMessage Post(Votacao votacao);
        HttpResponseMessage GetVoto(Votacao votacao);
    }
}
