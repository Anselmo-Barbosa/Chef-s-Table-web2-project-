using ChefsTable;
using ChefsTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chef_sTable.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ChefContext _context;

        public IndexModel(ChefContext context)
        {
            _context = context;
        }

        public List<Categoria> Categorias { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? CategoriaId { get; set; }


        public List<Receita> ReceitasDestaque { get; set; } = new();

        public async Task OnGetAsync()
        {
             Categorias = await _context.Categorias
            .OrderBy(c => c.Nome)
            .ToListAsync();

        IQueryable<Receita> query = _context.Receitas
            .Include(r => r.Usuario)
            .Include(r => r.Fotos);

        if (CategoriaId.HasValue)
        {
            query = query.Where(r => r.CategoriaId == CategoriaId.Value);
        }

        ReceitasDestaque = await query
            .Include(r => r.Usuario)
            .Include(r => r.Fotos)
            .Include(r => r.Comentarios)
            .OrderByDescending(r => r.Id)
            .Take(12)
            .ToListAsync();
        }
    }
}
