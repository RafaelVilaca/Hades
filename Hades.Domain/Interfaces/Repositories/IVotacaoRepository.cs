using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface IVotacaoRepository
    {
        void Post(Votacao votacao);
        void Put(Votacao votacao);
        Votacao GetVotos(int idUsuario, int idEnquete);
    }
}
