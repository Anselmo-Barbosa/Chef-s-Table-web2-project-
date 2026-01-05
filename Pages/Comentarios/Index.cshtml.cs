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
    public class IndexModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public IndexModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public IList<Comentario> Comentario { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Comentario = await _context.Comentarios
                .Include(c => c.Receita)
                .Include(c => c.Usuario).ToListAsync();
        }
    }
}
