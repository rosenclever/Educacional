using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Academico.Data;
using Academico.Models;
using static Academico.Models.AvaliacaoAlunoViewModel;

namespace Academico.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly EducacionalContext _context;

        public AvaliacaoController(EducacionalContext context)
        {
            _context = context;
        }

        // GET: Avaliacao
        public async Task<IActionResult> Index()
        {
            return View(await _context.Avaliacoes.ToListAsync());
        }

        // GET: Avaliacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.AvaliacaoID == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avaliacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvaliacaoID,Titulo")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avaliacao);
        }

        // GET: Avaliacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }
            return View(avaliacao);
        }

        // POST: Avaliacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvaliacaoID,Titulo")] Avaliacao avaliacao)
        {
            if (id != avaliacao.AvaliacaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.AvaliacaoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(avaliacao);
        }

        // GET: Avaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .FirstOrDefaultAsync(m => m.AvaliacaoID == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // POST: Avaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult LancarNota()
        {
            var Alunos = new SelectList(_context.Alunos, "AlunoId", "Nome");
            var viewModel = new AvaliacaoAlunoViewModel
            {
                Alunos = new SelectList(_context.Alunos, "AlunoId", "Nome").ToList(),
                Avaliacoes = new SelectList(_context.Avaliacoes, "AvaliacaoId", "Titulo")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarNota(AvaliacaoAlunoViewModel model)
        {
            // Validações básicas do modelo
            if (!ModelState.IsValid)
            {
                // Recarrega as listas se houver erro de validação
                model.Alunos = new SelectList(_context.Alunos, "AlunoId", "Nome");
                model.Avaliacoes = new SelectList(_context.Avaliacoes, "AvaliacaoId", "Titulo");
                return View("CadastrarNota", model);
            }

            // Tenta encontrar um relacionamento existente
            var alunoAvaliacao = await _context.AlunosAvaliacoes
                .FirstOrDefaultAsync(aa => aa.AlunoID == model.AlunoId && aa.AvaliacaoID == model.AvaliacaoId);

            if (alunoAvaliacao == null)
            {
                // Se o relacionamento NÃO existe, cria um novo
                alunoAvaliacao = new AlunoAvaliacao
                {
                    AlunoID = model.AlunoId,
                    AvaliacaoID = model.AvaliacaoId,
                    Nota = model.Nota
                };
                _context.AlunosAvaliacoes.Add(alunoAvaliacao);
            }
            else
            {
                // Se o relacionamento JÁ existe, atualiza a nota
                alunoAvaliacao.Nota = model.Nota;
                _context.AlunosAvaliacoes.Update(alunoAvaliacao);
            }

            await _context.SaveChangesAsync();

            // Redireciona para uma página de sucesso, como a de detalhes do aluno
            return RedirectToAction("DetalhesAluno", "Aluno", new { id = model.AlunoId });
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.AvaliacaoID == id);
        }
    }
}
