using System;
using System.Collections.Generic;

namespace Hades.Domain.Entities
{
    public class Enquete
    {
        public Enquete()
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
        public string Criador { get; set; }
        public string Nom_LocalCotado { get; set; }
        public decimal Valor { get; set; }

        //Votos
        public int VotoFavor { get; set; }
        public int VotoContra { get; set; }

        public List<Votacao> ListaVotacao { get; set; }
        //Votos        

        public int IsValid()
        {
            if (string.IsNullOrEmpty(Titulo) || this.Titulo.Trim().Length < 5)
                return 1;

            if (string.IsNullOrEmpty(Assunto) || this.Assunto.Trim().Length < 10)
                return 2;

            return 0;
        }
    }
}