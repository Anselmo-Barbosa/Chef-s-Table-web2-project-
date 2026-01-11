using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChefsTable;
using ChefsTable.Models;

namespace Chef_sTable.Pages.Usuarios
{
    public class CreateModel : PageModel
    {
        private readonly ChefsTable.ChefContext _context;

        public CreateModel(ChefsTable.ChefContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Usuario Usuario { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Usuarios.Add(Usuario);
            await _context.SaveChangesAsync();


            TempData["RegistroSucesso"] = "Conta criada com sucesso! Faça login.";
            return RedirectToPage("./Index");
        }
    }
}
