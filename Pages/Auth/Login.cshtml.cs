using ChefsTable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;



namespace Chef_sTable.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly ChefContext _context;

        public LoginModel(ChefContext context)
        {
            _context = context;
        }

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; }

        [BindProperty, Required]
        public string Senha { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == Email && u.Senha == Senha);



            if (usuario == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inválidos");
                return Page();
            }

            // Login temporário (mock)
            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);

            return RedirectToPage("/Receitas/Index");
        }
    }
}
