using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChefsTable.Models;
using ChefsTable;

namespace Chef_sTable.Pages.Receitas
{
    public class IndexModel : PageModel
    {
        private readonly ChefContext _context;

        public IndexModel(ChefContext context)
        {
            _context = context;
        }

        public IList<Receita> Receitas { get; set; } = new List<Receita>();

        public async Task OnGetAsync()
        {
            Receitas = await _context.Receitas.ToListAsync();
        }
    }
}
