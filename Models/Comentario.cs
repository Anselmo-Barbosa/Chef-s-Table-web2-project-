namespace Chef_sTable.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
        public Usuario User { get; set; }
        public int UserId { get; set; }


    }
}