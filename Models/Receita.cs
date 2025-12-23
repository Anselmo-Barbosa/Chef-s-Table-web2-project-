namespace Chef_sTable.Models
{
    public class Receita
    {
        private int Id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string ingredientes { get; set; }
        public string modoPreparo { get; set; }
        public int tempoPreparo { get; set; }
        public int userId { get; set; }
        public Usuario usuario { get; set; }
        public enum dificuldadeNivel
        {
            Facil,
            Medio,
            Dificil
        }

        public enum Tag
        {
            Vegetariano,
            SemGluten,
            LowCarb,
            Saudavel,
            Sobremesa,
            ZeroLactose,
            Festas,

        }
        public Categoria categoria { get; set; }

        public Avaliacao avaliacao;
        public DateTime dataCriacao { get; set; }
        public List<string> PhotoUrls { get; set; } 

        public ICollection<Comentario> comentarios = new List<Comentario>();
    }
}
