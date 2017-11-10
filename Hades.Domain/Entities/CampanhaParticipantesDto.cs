using System;

namespace Hades.Domain.Entities
{
    public class CampanhaParticipantesDto
    {
        public int IdUsuario { get; set; }
        public int IdCampanha { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
