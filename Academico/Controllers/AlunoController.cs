using Academico.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academico.Controllers
{
    public class AlunoController : Controller
    {
        private static List<Aluno> _alunos = new List<Aluno>();
        public IActionResult Index()
        {
            return View(_alunos);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    aluno.AlunoID = _alunos.Select(a => a.AlunoID).DefaultIfEmpty(0).Max()+1;
                    _alunos.Add(aluno);
                    return RedirectToAction("Index");
                }
                return View(aluno);
            }
            catch
            {
                return View(aluno);
            }
            
        }

        public IActionResult Edit(int id)
        {
            var aluno = _alunos.FirstOrDefault(a => a.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunos.Remove(_alunos.Where(a => a.AlunoID == aluno.AlunoID).First());
                _alunos.Add(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);
        }
        public IActionResult Details(int id)
        {
            var aluno = _alunos.FirstOrDefault(a => a.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        public IActionResult Delete(int id)
        {
            var aluno = _alunos.FirstOrDefault(a => a.AlunoID == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            
            try
            {
                var aluno = _alunos.FirstOrDefault(a => a.AlunoID == id);
                if(aluno == null)
                {
                    return NotFound();
                }
                _alunos.Remove(aluno);
                return RedirectToAction("Index");
            }
            catch
            {
                
            }
            return RedirectToAction("Index");
        }
    }
}
