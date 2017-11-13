using System;

namespace Hades.Domain.Entities
{
    public class CampanhaParticipantesDto
    {
        public int IdUsuario { get; set; }
        public string NomParticipante { get; set; }
        public int IdCampanha { get; set; }
    }
}
