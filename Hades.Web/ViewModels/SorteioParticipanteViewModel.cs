namespace Hades.Web.ViewModels
{
    public class SorteioParticipanteViewModel
    {
        public int Id_Participante { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public SorteioViewModel Sorteio { get; set; }
        public string Nome_Participante { get; set; }

        public SorteioParticipanteViewModel()
        {
            Usuario = new UsuarioViewModel();
            Sorteio = new SorteioViewModel();
        }
    }
}
