using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Comentarios
{
    public class CreateModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public CreateModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ReceitaId"] = new SelectList(_context.Receitas, "Id", "Id");
        ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Comentario Comentario { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comentarios.Add(Comentario);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
