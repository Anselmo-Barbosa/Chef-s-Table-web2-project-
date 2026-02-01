namespace ChefsTable.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public int Nota { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int ReceitaId { get; set; }
        public Receita? Receita { get; set; }
        public Usuario? Usuario { get; set; }
        public int UsuarioId { get; set; }


    }
}