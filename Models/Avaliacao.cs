namespace ChefsTable.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public string Feedback { get; set; }
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
        public Usuario Usuario { get; set; }
        public int UserId { get; set; }

    }
}