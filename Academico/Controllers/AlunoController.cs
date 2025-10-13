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
    public class AlunoController : Controller
    {
        private readonly EducacionalContext _context;

        public AlunoController(EducacionalContext context)
        {
            _context = context;
        }

        // GET: Aluno
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos.ToListAsync());
        }

        // GET: Aluno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .Include(a => a.AlunosAvaliacoes)
                .ThenInclude(aa => aa.Avaliacao)
                .FirstOrDefaultAsync(m => m.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Aluno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoID,Nome,Email,Cep")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlunoID,Nome,Email,Cep")] Aluno aluno)
        {
            if (id != aluno.AlunoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoID))
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
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CadastrarNota()
        {
            var viewModel = new AlunoAvaliacaoViewModel
            {
                Alunos = new SelectList(_context.Alunos, "AlunoId", "Nome"),
                Avaliacoes = new SelectList(_context.Avaliacoes, "AvaliacaoId", "Titulo")
            };
            return View(viewModel);
        }

        // Método POST: Processa o formulário de cadastro/edição
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarNota(AlunoAvaliacaoViewModel model)
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
                .FirstOrDefaultAsync(aa => aa.AlunoId == model.AlunoId && aa.AvaliacaoId == model.AvaliacaoId);

            if (alunoAvaliacao == null)
            {
                // Se o relacionamento NÃO existe, cria um novo
                alunoAvaliacao = new AlunoAvaliacao
                {
                    AlunoId = model.AlunoId,
                    AvaliacaoId = model.AvaliacaoId,
                    Nota = model.Nota
                };
                _context.AlunosAvaliacoes.Add(alunoAvaliacao);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Se o relacionamento JÁ existe, adiciona um erro ao ModelState
                ModelState.AddModelError(string.Empty, "Este aluno já possui uma nota para esta avaliação. Por favor, edite a nota existente em vez de tentar cadastrá-la novamente.");

                // Recarrega as listas e retorna para a view
                model.Alunos = new SelectList(_context.Alunos, "AlunoId", "Nome");
                model.Avaliacoes = new SelectList(_context.Avaliacoes, "AvaliacaoId", "Titulo");
                return View("CadastrarNota", model);
            }

            // Redireciona apenas se o cadastro for bem-sucedido
            TempData["SuccessMessage"] = "Nota cadastrada com sucesso!";
            return RedirectToAction("DetalhesAluno", "Aluno", new { id = model.AlunoId });
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.AlunoID == id);
        }
    }
}
