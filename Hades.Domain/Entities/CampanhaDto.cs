using System;
using System.Collections.Generic;

namespace Hades.Domain.Entities
{
    public class CampanhaDto
    {
        public CampanhaDto()
        {
            Participantes = new List<CampanhaParticipantesDto>();
        }

        public int IdCampanha { get; set; }
        public string DescCampanha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataLimite { get; set; }
        public decimal ValorCampanha { get; set; }
        public bool IndAtivo { get; set; }
        public int IdCriador { get; set; }

        public List<CampanhaParticipantesDto> Participantes { get; set; } 
    }
}
