namespace Hades.Web.ViewModels
{
    public class VotacaoViewModel
    {
        public int IdEnquete { get; set; }
        public int IdUsuario { get; set; }
        public string Enquete { get; set; }
        public string NomeUsuario { get; set; }
        public string Justificativa { get; set; }
        public bool TipoVoto { get; set; }
        public string IndicadorCadastro { get; set; }
        public string IndicadorTipoVoto => TipoVoto ? "S" : "N";
    }
}