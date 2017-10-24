using Hades.Domain.Entities;
using Hades.Domain.Interfaces.Repositories;
using Hades.Domain.Interfaces.Services;

namespace Hades.Domain.Services
{
    public class VotacaoService : IVotacaoService
    {
        private readonly IVotacaoRepository _votacaoRepository;

        public VotacaoService(IVotacaoRepository votacaoRepository)
        {
            _votacaoRepository = votacaoRepository;
        }

        public string Post(Votacao votacao)
        {
            _votacaoRepository.Post(votacao);
            return string.Empty;
        }

        public string Put(Votacao votacao)
        {
            _votacaoRepository.Put(votacao);
            return string.Empty;
        }

        public Votacao GetVotos(int idUsuario, int idEnquete)
        {
            return _votacaoRepository.GetVotos(idUsuario, idEnquete);
        }
    }
}
