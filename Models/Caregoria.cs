namespace Chef_sTable.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public ICollection<Receita> receitas { get; set; } = new List<Receita>();


    }
}
