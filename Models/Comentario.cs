namespace ChefsTable.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }


    }
}