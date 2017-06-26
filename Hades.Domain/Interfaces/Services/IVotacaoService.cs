using Hades.Domain.Entities;

namespace Hades.Domain.Interfaces.Services
{
    public interface IVotacaoService
    {
        string Put(Votacao votacao);
        string Post(Votacao votacao);
    }
}
