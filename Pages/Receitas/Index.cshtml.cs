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
    public class IndexModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public IndexModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public IList<Receita> Receita { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Receita = await _context.Receitas
                .Include(r => r.Fotos)
                .Include(r => r.Usuario).ToListAsync();
        }
    }
}
