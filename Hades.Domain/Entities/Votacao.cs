namespace Hades.Domain.Entities
{
    public class Votacao
    {
        public int Id { get; set; }
        public Enquete Enquetes { get; set; }
        public Usuario Usuarios { get; set; }
        public string Justificativa { get; set; }
        public bool TipoVoto { get; set; }
        public string Votador { get; set; }

        public int IsValid()
        {
            if (string.IsNullOrEmpty(Justificativa) && !TipoVoto
                || Justificativa.Trim().Length < 10 && !TipoVoto)
                return 1;

            return 0;
        }
    }
}