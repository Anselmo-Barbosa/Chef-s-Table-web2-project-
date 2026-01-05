using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Fotos
{
    public class DeleteModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public DeleteModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Foto Foto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foto = await _context.Fotos.FirstOrDefaultAsync(m => m.Id == id);

            if (foto is not null)
            {
                Foto = foto;

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

            var foto = await _context.Fotos.FindAsync(id);
            if (foto != null)
            {
                Foto = foto;
                _context.Fotos.Remove(Foto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
