namespace ChefsTable.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int RecipeId { get; set; }
        public Receita Receita { get; set; }
    }
}
