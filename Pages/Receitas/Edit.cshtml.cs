using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChefsTable;
using ChefsTable.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Chef_sTable.Pages.Receitas
{
    public class EditModel : PageModel
    {
        private readonly ChefContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(ChefContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Receita Receita { get; set; }

        [BindProperty]
        public List<IFormFile>? Imagens { get; set; }

        [BindProperty]
        public List<int>? ImagensRemovidas { get; set; }

        public List<Foto> ImagensAtuais { get; set; } = new();

        public SelectList CategoriasSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Receita = await _context.Receitas
                .Include(r => r.Fotos)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Receita == null)
                return NotFound();

            ImagensAtuais = Receita.Fotos.ToList();

            CategoriasSL = new SelectList(
                _context.Categorias,
                "Id",
                "Nome",
                Receita.CategoriaId
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CategoriasSL = new SelectList(_context.Categorias, "Id", "Nome");
                return Page();
            }

            var receitaDb = await _context.Receitas
                .Include(r => r.Fotos)
                .FirstOrDefaultAsync(r => r.Id == Receita.Id);

            if (receitaDb == null)
                return NotFound();

            
            receitaDb.Titulo = Receita.Titulo;
            receitaDb.Descricao = Receita.Descricao;
            receitaDb.Ingredientes = Receita.Ingredientes;
            receitaDb.ModoPreparo = Receita.ModoPreparo;
            receitaDb.TempoPreparo = Receita.TempoPreparo;
            receitaDb.Dificuldade = Receita.Dificuldade;
            receitaDb.Tags = Receita.Tags;
            receitaDb.CategoriaId = Receita.CategoriaId;

            // Remove imagens
            if (ImagensRemovidas?.Any() == true)
            {
                var fotosRemover = receitaDb.Fotos
                    .Where(f => ImagensRemovidas.Contains(f.Id))
                    .ToList();

                _context.Fotos.RemoveRange(fotosRemover);
            }

            // Adiciona imagens
            if (Imagens?.Any() == true)
            {
                foreach (var file in Imagens)
                {
                    var caminho = await SalvarImagemAsync(file);

                    receitaDb.Fotos.Add(new Foto
                    {
                        Url = caminho
                    });
                }
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Receita atualizada com sucesso!";

            return RedirectToPage("/Usuarios/Details");
        }

        private async Task<string> SalvarImagemAsync(IFormFile file)
        {
            var pasta = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(pasta);

            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var caminhoFisico = Path.Combine(pasta, nomeArquivo);

            using var stream = new FileStream(caminhoFisico, FileMode.Create);
            await file.CopyToAsync(stream);

            return "/uploads/" + nomeArquivo;
        }
    }
}