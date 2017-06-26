using System;

namespace Hades.Domain.Entities
{
    public class Enquete
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Assunto { get; set; }
        public DateTime DataEnquete { get; set; }
        public bool Ativo { get; set; }
        public Usuario Usuario { get; set; }

        public Enquete()
        {
            Usuario = new Usuario();
        }

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