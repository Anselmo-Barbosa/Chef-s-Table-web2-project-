using System.Xml.Linq;

namespace Chef_sTable.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ICollection<Receita> Receitas { get; set; } = new List<Receita>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}
