using ChefsTable;
using ChefsTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chef_sTable.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ChefContext _context;

        public RegisterModel(ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Nome { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Senha { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

           
            if (_context.Usuarios.Any(u => u.Email == Email))
            {
                ModelState.AddModelError("", "Este email já está cadastrado.");
                return Page();
            }

            var usuario = new Usuario
            {
                Nome = Nome,
                Email = Email,
                Senha = Senha // depois criptografamos
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            TempData["RegistroSuccess"] = "Cadastro realizado com sucesso! Faça login.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
