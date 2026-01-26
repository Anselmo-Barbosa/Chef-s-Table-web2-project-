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
        public IList<Receita> Receita { get; set; } = new List<Receita>();

        public async Task<IActionResult> OnGetAsync()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            Usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (Usuario == null)
                return NotFound();

            Receita = await _context.Receitas
            .Where(r => r.UsuarioId == usuarioId)
            .Include(r => r.Fotos)
            .OrderByDescending(r => r.Id)
            .ToListAsync();

            return Page();
        }
    }
}
