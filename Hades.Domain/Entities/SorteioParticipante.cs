namespace Hades.Domain.Entities
{
    public class SorteioParticipante
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Sorteio Sorteio { get; set; }

        public SorteioParticipante()
        {
            Usuario = new Usuario();
            Sorteio = new Sorteio();
        }
    }
}
