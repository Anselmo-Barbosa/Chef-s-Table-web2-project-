using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefsTable;
using ChefsTable.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Chef_sTable.Pages.Receitas
{
    public class DetailsModel : PageModel
    {
        private readonly ChefContext _context;

        public DetailsModel(ChefContext context)
        {
            _context = context;
        }

        // DADOS DA PÁGINA 
        public Receita Receita { get; set; } = default!;
        public List<Comentario> Comentarios { get; set; } = new();

        [BindProperty]
        public int Nota { get; set; }

        public double MediaAvaliacao { get; set; }

        //FORM DE COMENTÁRIO 
        [BindProperty]
        [Required(ErrorMessage = "O comentário não pode estar vazio.")]
        public string NovoComentario { get; set; } = string.Empty;

        public int? UsuarioLogadoId => HttpContext.Session.GetInt32("UsuarioId");

        [BindProperty]
        public int ComentarioEmEdicaoId { get; set; }

        [BindProperty]
        public string TextoEdicao { get; set; } = string.Empty;

        [BindProperty]
        public int NotaEdicao { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Receita = await _context.Receitas
                .Include(r => r.Usuario)
                .Include(r => r.Fotos)
                .FirstOrDefaultAsync(r => r.Id == id.Value);

          
            Comentarios = await _context.Comentarios
                .Include(c => c.Usuario)
                .Where(c => c.ReceitaId == id.Value)
                .OrderByDescending(c => c.DataCriacao)
                .ToListAsync();

            if (Receita == null)
                return NotFound();


            if (Comentarios.Any())
            {
                MediaAvaliacao = Comentarios.Average(c => c.Nota);
            }
            else
            {
                MediaAvaliacao = 0;
            }


            return Page();
        }

        // ===== POST: COMENTAR =====
        public async Task<IActionResult> OnPostComentarioAsync(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            bool jaComentou = await _context.Comentarios
            .AnyAsync(c => c.ReceitaId == id && c.UsuarioId == usuarioId.Value);

            if (jaComentou)
            {
                ModelState.AddModelError(string.Empty, "Você já avaliou esta receita.");
                await OnGetAsync(id);
                return Redirect($"/Receitas/Details?id={id}#comentarios");
            }
       
            if (Nota < 1 || Nota > 5)
            {
                ModelState.AddModelError("Nota", "Selecione uma nota de 1 a 5.");
                await OnGetAsync(id);
                return Redirect($"/Receitas/Details?id={id}#comentarios");
            }

           

            var comentario = new Comentario
            {
                Texto = NovoComentario,
                ReceitaId = id,
                UsuarioId = usuarioId.Value,
                Nota = Nota,
                DataCriacao = DateTime.Now
            };

            _context.Comentarios.Add(comentario);

            Console.WriteLine($"ReceitaId={comentario.ReceitaId}, UsuarioId={comentario.UsuarioId}");

            await _context.SaveChangesAsync();

            return Redirect($"/Receitas/Details?id={id}#comentarios");
        }

        public async Task<IActionResult> OnPostExcluirComentarioAsync(int comentarioId, int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(c => c.Id == comentarioId);

            if (comentario == null)
                return Forbid();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return Redirect($"/Receitas/Details?id={id}#comentarios");
        }

        public async Task<IActionResult> OnPostEditarComentarioAsync(int id, int comentarioId)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(c =>
                    c.Id == comentarioId &&
                    c.UsuarioId == usuarioId.Value);


            ComentarioEmEdicaoId = comentario.Id;
            TextoEdicao = comentario.Texto;

            await OnGetAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostSalvarEdicaoAsync(int id)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");

            if (usuarioId == null)
                return RedirectToPage("/Auth/Login");

            var comentario = await _context.Comentarios
                .FirstOrDefaultAsync(c =>
                    c.Id == ComentarioEmEdicaoId &&
                    c.UsuarioId == usuarioId.Value);

            comentario.Texto = TextoEdicao;

            await _context.SaveChangesAsync();

            return Redirect($"/Receitas/Details?id={id}#comentarios");
        }
    }
}