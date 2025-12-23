namespace Chef_sTable.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int nota { get; set; }
        public string feedback { get; set; }
        public int receitaId { get; set; }
        public Receita receita { get; set; }
        public Usuario usuario { get; set; }
        public int userId { get; set; }

    }
}