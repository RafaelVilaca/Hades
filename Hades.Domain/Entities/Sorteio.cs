namespace Hades.Domain.Entities
{
    public class Sorteio
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int IsValid()
        {
            if (string.IsNullOrEmpty(Nome) || this.Nome.Trim().Length < 5)
                return 1;

            return 0;
        }
    }
}
