using Microsoft.AspNetCore.Mvc;
using academico.Models;

namespace academico.Controllers
{
    public class AlunoController : Controller
    {
        private static List<Aluno> alunos = new List<Aluno>();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    alunos.Add(aluno);
                    return RedirectToAction(nameof(Index));
                }
            }catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocorreu um erro ao criar o aluno: {ex.Message}");
            }
            return View(aluno);
        }


        public IActionResult Index()
        {
            return View(alunos);
        }

    }
}
