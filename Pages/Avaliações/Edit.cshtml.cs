using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Avaliações
{
    public class EditModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public EditModel(ChefsTable.ChefContext context)
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

            var avaliacao =  await _context.Avaliacoes.FirstOrDefaultAsync(m => m.Id == id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            Avaliacao = avaliacao;
           ViewData["ReceitaId"] = new SelectList(_context.Receitas, "Id", "Id");
           ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Avaliacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvaliacaoExists(Avaliacao.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.Id == id);
        }
    }
}
