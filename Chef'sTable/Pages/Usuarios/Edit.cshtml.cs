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

namespace Chef_sTable.Pages.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public EditModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario =  await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            Usuario = usuario;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // Ignora validação da senha (não vem no formulário)
            ModelState.Remove("Usuario.Senha");

            if (!ModelState.IsValid)
                return Page();

            var usuarioDb = await _context.Usuarios.FindAsync(Usuario.Id);

            if (usuarioDb == null)
                return NotFound();

            usuarioDb.Nome = Usuario.Nome;
            usuarioDb.Email = Usuario.Email;

            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Perfil atualizado com sucesso!";

            return RedirectToPage("./Details", new { id = Usuario.Id });
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
