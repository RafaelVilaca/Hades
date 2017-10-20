namespace Hades.Domain.Entities
{
    public class Votacao
    {
        public int Id { get; set; }
        public int IdEnquete { get; set; }
        public int IdUsuario { get; set; }
        public string Enquete { get; set; }
        public string NomeUsuario { get; set; }
        public string Justificativa { get; set; }
        public bool TipoVoto { get; set; }

        public int IsValid()
        {
            if (string.IsNullOrEmpty(Justificativa) && !TipoVoto
                || Justificativa.Trim().Length < 10 && !TipoVoto)
                return 1;

            return 0;
        }
    }
}