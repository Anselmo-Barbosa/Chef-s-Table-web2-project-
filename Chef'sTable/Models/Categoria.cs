namespace ChefsTable.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Receita> receitas { get; set; } = new List<Receita>();

    }
}
