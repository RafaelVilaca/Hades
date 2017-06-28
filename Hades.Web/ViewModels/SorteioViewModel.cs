using System.ComponentModel;

namespace Hades.Web.ViewModels
{
    public class SorteioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [DisplayName("N° Participantes")]
        public int QtdParticipantes { get; set; }

        public int IsValid()
        {
            if (string.IsNullOrEmpty(Nome) || this.Nome.Trim().Length < 5)
                return 1;

            return 0;
        }
    }
}
