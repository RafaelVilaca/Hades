using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Hades.Web.ViewModels
{
    public class VotacaoViewModel
    {
        public int Id { get; set; }
        public EnqueteViewModel Enquetes { get; set; }
        public UsuarioViewModel Usuarios { get; set; }
        public string Justificativa { get; set; }
        public bool TipoVoto { get; set; }

        public List<EnqueteViewModel> ListaDropDown;

        public SelectList Bunda => new SelectList(ListaDropDown,"Id","Titulo");
        
        public int IsValid()
        {
            if (string.IsNullOrEmpty(Justificativa) && !TipoVoto
                || Justificativa.Trim().Length < 10 && !TipoVoto)
                return 1;

            return 0;
        }
    }
}