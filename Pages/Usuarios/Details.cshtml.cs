using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Usuarios
{
    public class DetailsModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public DetailsModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);

            if (usuario is not null)
            {
                Usuario = usuario;

                return Page();
            }

            return NotFound();
        }
    }
}
