using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Receitas
{
    public class DeleteModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public DeleteModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Receita Receita { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas.FirstOrDefaultAsync(m => m.Id == id);

            if (receita is not null)
            {
                Receita = receita;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas.FindAsync(id);
            if (receita != null)
            {
                Receita = receita;
                _context.Receitas.Remove(Receita);
                await _context.SaveChangesAsync();
            }

            TempData["MensagemDelete"] = "Sua receita foi deletada com sucesso!";

            return RedirectToPage("/Usuarios/Details");
        }
    }
}
