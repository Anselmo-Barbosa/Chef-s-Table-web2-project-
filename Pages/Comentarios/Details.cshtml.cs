using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Comentarios
{
    public class DetailsModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public DetailsModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public Comentario Comentario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios.FirstOrDefaultAsync(m => m.Id == id);

            if (comentario is not null)
            {
                Comentario = comentario;

                return Page();
            }

            return NotFound();
        }
    }
}
