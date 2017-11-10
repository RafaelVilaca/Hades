using System;
using System.Collections.Generic;

namespace Hades.Domain.Entities
{
    public class EnqueteDto
    {
        public EnqueteDto()
        {
            Usuario = new Usuario();
            ListaVotacao = new List<Votacao>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Assunto { get; set; }
        public DateTime DataEnquete { get; set; }
        public bool Ativo { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string Criador { get; set; }
        public string Nom_LocalCotado { get; set; }
        public decimal Valor { get; set; }
        public bool Adm { get; set; }
        public string NomeVerificacao { get; set; }

        public string DataFormatada => $"{DataEnquete:dd/MM/yyyy}";

        //Votos
        public int VotoFavor { get; set; }
        public int VotoContra { get; set; }

        public List<Votacao> ListaVotacao { get; set; }
    }
}