using Hades.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hades.Web.ViewModels
{
    public class CampanhaViewModel
    {
        public CampanhaViewModel()
        {
            Participantes = new List<CampanhaParticipanteViewModel>();
        }

        public CampanhaViewModel(CampanhaDto campanhaDto)
        {
            IdCampanha = campanhaDto.IdCampanha;
            DescCampanha = campanhaDto.DescCampanha;
            DataCadastro = campanhaDto.DataCadastro;
            DataLimite = campanhaDto.DataLimite;
            ValorCampanha = campanhaDto.ValorCampanha;
            IndAtivo = campanhaDto.IndAtivo;
            IdCriador = campanhaDto.IdCriador;
            NumeroParticipantes = campanhaDto.NumeroParticipantes;
            Participantes = new List<CampanhaParticipanteViewModel>();
        }

        public int IdCampanha { get; set; }
        public string DescCampanha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataLimite { get; set; }
        public decimal ValorCampanha { get; set; }
        public bool IndAtivo { get; set; }
        public int IdCriador { get; set; }
        public int NumeroParticipantes { get; set; }
        public string IndParticipante { get; set; }
        public string DataFormatada => $"{DataLimite:dd/MM/yyyy}";
        public string DataCadFormatada => $"{DataCadastro:dd/MM/yyyy}";

        public List<CampanhaParticipanteViewModel> Participantes { get; set; } 
    }
}
