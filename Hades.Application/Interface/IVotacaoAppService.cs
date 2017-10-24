using Hades.Domain.Entities;
using System.Net.Http;

namespace Hades.Application.Interface
{
    public interface IVotacaoAppService
    {
        HttpResponseMessage Post(Votacao votacao);
        HttpResponseMessage Put(Votacao votacao);
        HttpResponseMessage GetVotos(int? idUsuario, int? idEnquete);
    }
}
