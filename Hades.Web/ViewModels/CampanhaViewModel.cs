using System;
using System.Collections.Generic;

namespace Hades.Domain.Entities
{
    public class CampanhaViewModel
    {
        public CampanhaViewModel()
        {
            Participantes = new List<CampanhaParticipanteViewModel>();
        }

        public int IdCampanha { get; set; }
        public string DescCampanha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataLimite { get; set; }
        public decimal ValorCampanha { get; set; }
        public bool IndAtivo { get; set; }
        public int IdCriador { get; set; }

        public List<CampanhaParticipanteViewModel> Participantes { get; set; } 
    }
}
