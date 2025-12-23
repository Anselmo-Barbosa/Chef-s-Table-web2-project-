using System.Xml.Linq;

namespace Chef_sTable.Models
{
    public class Usuario
    {
        private int Id { get; set; } 
        public string nome { get; set; }
        public string email { get; set; }
        private string senha { get; set; }
        public ICollection<Receita> receitas { get; set; } = new List<Receita>();
        public ICollection<Comentario> comentarios { get; set; } = new List<Comentario>();
        public ICollection<Avaliacao> avaliacoes { get; set; } = new List<Avaliacao>();
    }
}
