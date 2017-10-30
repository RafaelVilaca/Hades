namespace Hades.Domain.Entities
{
    public class SorteioParticipante
    {
        public int Id_Participante { get; set; }
        public Usuario Usuario { get; set; }
        public Sorteio Sorteio { get; set; }
        public string Nome_Participante { get; set; }

        public SorteioParticipante()
        {
            Usuario = new Usuario();
            Sorteio = new Sorteio();
        }
    }
}
