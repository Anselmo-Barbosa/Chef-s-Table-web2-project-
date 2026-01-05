using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Avaliações
{
    public class DeleteModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public DeleteModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Avaliacao Avaliacao { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.FirstOrDefaultAsync(m => m.Id == id);

            if (avaliacao is not null)
            {
                Avaliacao = avaliacao;

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

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                Avaliacao = avaliacao;
                _context.Avaliacoes.Remove(Avaliacao);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
