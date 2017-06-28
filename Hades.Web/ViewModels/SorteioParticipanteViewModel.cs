using System.ComponentModel;

namespace Hades.Web.ViewModels
{
    public class SorteioParticipanteViewModel
    {
        public int Id { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public SorteioViewModel Sorteio { get; set; }
        public string NomeUsuario { get; set; }

        public SorteioParticipanteViewModel()
        {
            Usuario = new UsuarioViewModel();
            Sorteio = new SorteioViewModel();
        }
    }
}
