using System.ComponentModel.DataAnnotations;
using static ChefsTable.Models.Receita;

namespace ChefsTable.Models
{
    public class Receita
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Os ingredientes são necessarios")]
        public string Ingredientes { get; set; }

        [Required(ErrorMessage = "É necessario detalhar o modo de preparo")]
        [Display(Name = "Modo de Preparo")]
        public string ModoPreparo { get; set; }

        [Required(ErrorMessage = "O tempo deve ser entre 1 e 600 minutos")]
        [Display(Name = "Tempo de Preparo (em minutos)")]
        [Range(1, 600, ErrorMessage = "O tempo deve ser entre 1 e 600 minutos")]
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
        [Required(ErrorMessage = "A categoria é obrigatória")]
        public int CategoriaId { get; set; }
        
        public Categoria? Categoria { get; set; }

        [Display(Name = "Data De criação")]
        public DateTime DataCriacao { get; set; }
        public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
