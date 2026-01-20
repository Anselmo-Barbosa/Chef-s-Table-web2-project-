using System.ComponentModel.DataAnnotations;
using static ChefsTable.Models.Receita;

namespace ChefsTable.Models
{
    public class Receita
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        public string Ingredientes { get; set; }

        [Required]
        [Display(Name = "Modo de Preparo")]
        public string ModoPreparo { get; set; } 

        [Required]
        [Display(Name = "Tempo de Preparo")]
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

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        [Display(Name = "Data De criação")]
        public DateTime DataCriacao { get; set; }
        public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
