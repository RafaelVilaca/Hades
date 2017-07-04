using System.Collections.Generic;
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
            if (votacao.IsValid() != 0)
                return "Em caso de voto contra, a Justificativa é obrigatório ter de 10 à 200 caracteres";

            _votacaoRepository.Post(votacao);
            return string.Empty;
        }
    }
}
