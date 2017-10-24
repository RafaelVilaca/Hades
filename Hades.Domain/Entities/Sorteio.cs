namespace Hades.Domain.Entities
{
    public class Sorteio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdParticipantes { get; set; }
        public int IdUsuario { get; set; }
        public bool Adm { get; set; }

    }
}
