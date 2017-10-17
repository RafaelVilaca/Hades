namespace Hades.Domain.Entities
{
    public class UsuarioLogado
    {
        public UsuarioLogado(int id, string nome, bool verifica)
        {
            Id = id;
            Nome = nome;
            Administrador = verifica;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Administrador { get; set; }
    }
}
