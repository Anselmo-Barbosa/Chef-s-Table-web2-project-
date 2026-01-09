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
    public class DetailsModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public DetailsModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public Receita Receita { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .Include(r => r.Fotos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (receita is not null)
            {
                Receita = receita;

                return Page();
            }

            return NotFound();
        }
    }
}
