using ChefsTable;
using ChefsTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Chef_sTable.Pages.Receitas
{
    public class CreateModel : PageModel
    {
        private readonly ChefContext _context;

        public CreateModel(ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Receita Receita { get; set; } = default!;

        public SelectList UsuariosSL { get; set; } = default!;
        public SelectList CategoriasSL { get; set; } = default!;

        public IActionResult OnGet()
        {
            Receita = new Receita();

            UsuariosSL = new SelectList(
                _context.Usuarios.ToList(),
                "Id",
                "Nome"
            );

            CategoriasSL = new SelectList(
                _context.Categorias.ToList(),
                "Id",
                "Nome"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                UsuariosSL = new SelectList(
                    _context.Usuarios.ToList(),
                    "Id",
                    "Nome",
                    Receita.UsuarioId
                );
                CategoriasSL = new SelectList(
                    _context.Categorias.ToList(),
                    "Id",
                    "Nome",
                    Receita.CategoriaId
                );

                return Page();
            }

            _context.Receitas.Add(Receita);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}