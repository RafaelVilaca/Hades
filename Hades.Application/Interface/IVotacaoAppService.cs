using System.Net.Http;
using Hades.Domain.Entities;

namespace Hades.Application.Interface
{
    public interface IVotacaoAppService
    {
        HttpResponseMessage Post(Votacao votacao);
    }
}
