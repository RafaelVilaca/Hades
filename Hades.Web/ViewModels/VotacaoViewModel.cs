using System.Collections.Generic;
using System.Web.Mvc;

namespace Hades.Web.ViewModels
{
    public class VotacaoViewModel
    {
        public int Id { get; set; }
        public EnqueteViewModel Enquetes { get; set; }
        public UsuarioViewModel Usuarios { get; set; }
        public string Justificativa { get; set; }
        public bool TipoVoto { get; set; }
        public string Votador { get; set; }

        public List<EnqueteViewModel> ListaDropDown;

        public SelectList ListaParaVotacao => new SelectList(ListaDropDown,"Id","Titulo");
        
        public int IsValid()
        {
            if (string.IsNullOrEmpty(Justificativa) && !TipoVoto
                || Justificativa.Trim().Length < 10 && !TipoVoto)
                return 1;

            return 0;
        }
    }
}