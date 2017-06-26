using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Repositories
{
    public interface IVotacaoRepository
    {
        void Put(Votacao votacao);
        void Post(Votacao votacao);
    }
}
