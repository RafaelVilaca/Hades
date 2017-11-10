namespace Hades.Domain.Entities
{
    public class SorteioParticipanteDto
    {
        public int Id_Participante { get; set; }
        public Usuario Usuario { get; set; }
        public SorteioDto Sorteio { get; set; }
        public string Nome_Participante { get; set; }

        public SorteioParticipanteDto()
        {
            Usuario = new Usuario();
            Sorteio = new SorteioDto();
        }
    }
}
