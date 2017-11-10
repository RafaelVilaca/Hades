using System;
using System.Collections.Generic;

namespace Hades.Domain.Entities
{
    public class SorteioDto
    {
        public SorteioDto()
        {
            SorteioParticipantes = new List<SorteioParticipanteDto>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdParticipantes { get; set; }
        public int IdCriador { get; set; }
        public string NomeCriador { get; set; }
        public bool Adm { get; set; }
        public DateTime DataSorteio { get; set; }
        public int QtdeItens { get; set; }
        public string IndParticipante { get; set; }
        public bool Ativo { get; set; }
        public bool FoiSorteado { get; set; }
        public List<SorteioParticipanteDto> SorteioParticipantes { get; set; }
    }
}
