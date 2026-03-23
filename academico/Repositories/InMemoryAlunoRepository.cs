using academico.Models;
using academico.Repositories;

namespace academico.Repositories
{
    public class InMemoryAlunoRepository : IAlunoRepository
    {
        private readonly List<Aluno> _alunos = new List<Aluno>;
        private int _nextId = 1;
        private readonly object _lock = new object();

        public InMemoryAlunoRepository()
        {
            _alunos.Add(new Aluno
            {
                AlunoId = _nextId++,
                Nome = "Aluno Exemplo",
                Email = "aluno@exemplo.com",
                Telefone = "(11) 99999-9999",
                Endereco = "Rua Exemplo, 123",
                Complemento = "Bloco A",
                Bairro = "Centro",
                Municipio = "Cidade",
                Uf = "RJ",
                Cep = "01234-567"
            });
        }
    
        public Task Create(Aluno aluno, CancellationToken cancellation = default)
        {
            lock (_lock)
            {
                aluno.AlunoId = _nextId++;
                _alunos.Add(aluno);
            }
            return Task.CompletedTask;
        }
        public Task Delete(int id, CancellationToken cancellation = default)
        {
            lock (_lock)
            {
                var exising = _alunos.FirstOrDefault(a => a.AlunoId == id);
                if (exising != null)
                    _alunos.Remove(exising);
            }
            return Task.CompletedTask;
        }
        public Task<Enumerable<Aluno>> GetAll(CancellationToken cancellation = default)
        {
            IEnumerable<Aluno> result;
            lock (_lock)
            {
                result = _alunos.Select(a => a).ToList();
            }
            return Task.FromResult(result);
        }

        public Task<Aluno?> GetById(int id, CancellationToken cancellationToken = default)
        {
            Aluno? aluno;
            lock (_lock)
            {
                aluno = _alunos.FirstOrDefault(a =>a.AlunoId == id);
            }
            return Task.FromResult(aluno);
        }
        public Task Edit(Aluno aluno, CancellationToken cancellation = default)
        {
            lock (_lock)
            {
                var existing = _alunos.FirstOrDefault(a => a.AlunoId == aluno.AlunoId);
                if (existing != null)
                {
                    existing.Nome = aluno.Nome;
                    existing.Email = aluno.Email;
                    existing.Telefone = aluno.Telefone;
                    existing.Endereco = aluno.Endereco;
                    existing.Complemento = aluno.Complemento;
                    existing.Bairro = aluno.Bairro;
                    existing.Municipio = aluno.Municipio;
                    existing.Uf = aluno.Uf;
                    existing.Cep = aluno.Cep;
                }
            }
            return Task.CompletedTask;
        }
        public Task<bool> Exists(int id, CancellationToken cancellation = default)
        {
            bool exists;
            lock (_lock)
            {
                exists = _alunos.Any(a => a.AlunoId == id);
            }
            return Task.FromResult(exists);
        }
}
