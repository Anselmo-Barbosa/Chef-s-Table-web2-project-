using ChefsTable;
using ChefsTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Chef_sTable.Pages.Receitas
{
    public class CreateModel : PageModel
    {
        private readonly ChefContext _context;

        public CreateModel(ChefContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Receita Receita { get; set; } = new();

        [BindProperty]
        public List<IFormFile> Imagens { get; set; } = new();

        public SelectList CategoriasSL { get; set; } = default!;

        public IActionResult OnGet()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            // Só cria receita logado
            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            CategoriasSL = new SelectList(
                _context.Categorias.ToList(),
                "Id",
                "Nome"
            );

            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            if (!ModelState.IsValid)
            {
                CategoriasSL = new SelectList(
                    _context.Categorias.ToList(),
                    "Id",
                    "Nome",
                    Receita.CategoriaId
                );

                return Page();
            }

            // Usuário logado
            Receita.UsuarioId = usuarioId.Value;
            Receita.DataCriacao = DateTime.Now;

            _context.Receitas.Add(Receita);

            // Upload de imagens
          
            if (Imagens != null && Imagens.Count > 0)
            {
                string pastaUploads = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/receitas"
                );

                if (!Directory.Exists(pastaUploads))
                    Directory.CreateDirectory(pastaUploads);

                foreach (var imagem in Imagens)
                {
                    string nomeArquivo = Guid.NewGuid() + Path.GetExtension(imagem.FileName);
                    string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

                    using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                    await imagem.CopyToAsync(stream);

                    Receita.Fotos.Add(new Foto
                    {
                        Url = "/uploads/receitas/" + nomeArquivo
                    });
                }
            }

            await _context.SaveChangesAsync();

            TempData["ReceitaSuccess"] = "Sua receita foi postada com sucesso!";

            return RedirectToPage("/Index");
        }
    }
}