using Hades.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Hades.Web.ViewModels
{
    public class EnqueteViewModel
    {
        public EnqueteViewModel()
        {
            Usuario = new UsuarioViewModel();
        }
        
        public EnqueteViewModel(Enquete enquete)
        {
            Id = enquete.Id;
            Titulo = enquete.Titulo;
            Assunto = enquete.Assunto;
            DataEnquete = enquete.DataEnquete;
            Ativo = enquete.Ativo;
            Criador = enquete.Criador;
            Nom_LocalCotado = enquete.Nom_LocalCotado;
            Valor = enquete.Valor;
            Usuario = new UsuarioViewModel();
            ListaVotacao = new List<VotacaoViewModel>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Assunto { get; set; }
        public DateTime DataEnquete { get; set; }
        public bool Ativo { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string Criador { get; set; }
        public string Nom_LocalCotado { get; set; }
        public decimal Valor { get; set; }

        public string DataFormatada => $"{DataEnquete:dd/MM/yyyy}";
        //Votos
        public int VotoFavor { get; set; }
        public int VotoContra { get; set; }

        public List<VotacaoViewModel> ListaVotacao { get; set; }
    }
}