namespace Chef_sTable.Models
{
    public class Comentario
    {
        private int Id { get; set; }
        public string texto { get; set; }
        public DateTime dataCriacao { get; set; }
        public int receitaId { get; set; }
        public Receita receita { get; set; }
        public Usuario user { get; set; }
        public int userId { get; set; }


    }
}