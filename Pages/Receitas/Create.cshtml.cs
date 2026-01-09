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
        public Receita Receita { get; set; } = default!;

        [BindProperty]
        public List<IFormFile> Imagens { get; set; } = new();
        public SelectList UsuariosSL { get; set; } = default!;
        public SelectList CategoriasSL { get; set; } = default!;

        public IActionResult OnGet()
        {
            Receita = new Receita();

            UsuariosSL = new SelectList(
                _context.Usuarios.ToList(),
                "Id",
                "Nome"
            );

            CategoriasSL = new SelectList(
                _context.Categorias.ToList(),
                "Id",
                "Nome"
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
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
            //Mocagem temporaria
            Receita.UsuarioId = 1; 

            Receita.DataCriacao = DateTime.Now;

            _context.Receitas.Add(Receita);

            if (Imagens != null && Imagens.Count > 0)
            {
                string pastaUploads = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/receitas");

                if (!Directory.Exists(pastaUploads))
                    Directory.CreateDirectory(pastaUploads);

                foreach (var imagem in Imagens)
                {
                    string nomeArquivo = Guid.NewGuid() + Path.GetExtension(imagem.FileName);
                    string caminhoCompleto = Path.Combine(pastaUploads, nomeArquivo);

                    using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    Receita.Fotos.Add(new Foto
                    {
                        Url = "/uploads/receitas/" + nomeArquivo
                    });
                }
                }
                await _context.SaveChangesAsync();
            
            TempData["Success"] = "Sua receita foi postada com sucesso!";

            return RedirectToPage("./Index");
        }
    }
}