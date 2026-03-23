using Microsoft.AspNetCore.Mvc;
using academico.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using academico.Repositories;

namespace academico.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoRepository _alunoRepository;
        public AlunoController(IAlunoRepository repository)
        {
            _alunoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<IActionResult> Cretae()
        {
            return View(new Aluno());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (!ModelState.IsValid)
            {
                return View(aluno);
            }
            await _alunoRepository.Create(aluno);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoRepository.GetAll();
            return View(alunos);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Aluno aluno)
        {
            if (id != aluno.AlunoId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(aluno);
            }

            await _alunoRepository.Edit(aluno);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        public async Task<IActionResult> Delete (int id)
        {
            var aluno = await _alunoRepository.GetById(id);
            if(aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _alunoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private static List<Aluno> alunos = new List<Aluno>()
        {
            new Aluno()
            {
                AlunoId = 1,
                Nome = "Teste",
                Email = "teste@as",
                Telefone = "12 99999-8888",
                Endereco = "rua",
                Bairro = "bairro",
                Municipio = "cidade",
                Uf = "SE",
                Cep = "29181-000"
            }
        };

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
                    aluno.AlunoId = alunos.Select(a => a.AlunoId).DefaultIfEmpty(0).Max()+1;
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

        public IActionResult Edit(int id) 
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AlunoID, Nome, Email, Telefone, Endereço, Complemento, Bairro, Municipio, Uf, Cep")] Aluno aluno)
        {
            try
            {
                if (id != aluno.AlunoId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    var existingAluno = alunos.FirstOrDefault(a => a.AlunoId == id);
                    if (existingAluno == null)
                    {
                        return NotFound();
                    }
                    existingAluno.Nome = aluno.Nome;
                    existingAluno.Email = aluno.Email;
                    existingAluno.Telefone = aluno.Telefone;
                    existingAluno.Endereco = aluno.Endereco;
                    existingAluno.Complemento = aluno.Complemento;
                    existingAluno.Bairro = aluno.Bairro;
                    existingAluno.Municipio = aluno.Municipio;
                    existingAluno.Uf = aluno.Uf;
                    existingAluno.Cep = aluno.Cep;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ocorreu um erro ao editar o aluno: {ex.Message}");
            }
            return View(aluno);
        }
        public IActionResult Details(int id)
        {
            var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var aluno = alunos.FirstOrDefault(a => a.AlunoId == id);
                if (aluno == null)
                {
                    return NotFound();
                }
                alunos.Remove(aluno);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Não foi possível excluir o aluno: {ex.Message}")
            }
            return View(alunos);
        }
    }
}
