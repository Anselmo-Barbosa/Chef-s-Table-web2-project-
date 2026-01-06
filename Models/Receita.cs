using System.ComponentModel.DataAnnotations;
using static ChefsTable.Models.Receita;

namespace ChefsTable.Models
{
    public class Receita
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }
        
        [Range(1, 300)]
        public string Descricao { get; set; }
    
        [Range(1, 500)]
        public string Ingredientes { get; set; }

        [Required]
        public string ModoPreparo { get; set; }

        [Required]
        public int TempoPreparo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public enum DificuldadeNivel
        {
            Facil,
            Medio,
            Dificil
        }
        [Required]

        public DificuldadeNivel Dificuldade { get; set; }

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

        public Tag? Tags { get; set; }

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        public DateTime DataCriacao { get; set; }
        public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
