using System;
using System.Collections.Generic;

namespace Hades.Web.ViewModels
{
    public class SorteioViewModel
    {
        public SorteioViewModel()
        {
            SorteioParticipantes = new List<SorteioParticipanteViewModel>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdParticipantes { get; set; }
        public int IdCriador { get; set; }
        public string NomeCriador { get; set; }
        public bool Adm { get; set; }
        public DateTime DataSorteio { get; set; }
        public string DataFormatada => $"{DataSorteio:dd/MM/yyyy}";
        public int QtdeItens { get; set; }
        public string IndParticipante { get; set; }
        public bool Ativo { get; set; }

        public List<SorteioParticipanteViewModel> SorteioParticipantes { get; set; }
    }
}