using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface IVotacaoService
    {
        string Post(Votacao votacao);
        string Put(Votacao votacao);
        Votacao GetVotos(int idUsuario, int idEnquete);
    }
}
