using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;

namespace CursoIdiomasApp.Domain.Services
{
    internal class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
    {
        public AlunoResponse CreateAluno(AlunoRequest alunoRequest)
        {
            var aluno = new Aluno
            (
                alunoRequest.nome,
                alunoRequest.cpf,
                alunoRequest.email
            );

            alunoRepository.Create(aluno);

            return new AlunoResponse(aluno.Id, aluno.Nome, aluno.Cpf, aluno.Email);
        }

        public AlunoDeleteResponse DeleteAluno(Guid id)
        {
            var aluno = alunoRepository.GetById(id);

            if (aluno == null) {
                throw new ApplicationException("Aluno não encontrado");
            }

            alunoRepository.Delete(aluno);

            return new AlunoDeleteResponse("Aluno deletado com sucesso");
        }

        public List<AlunoResponse> GetAllAlunos()
        {
            var alunos = alunoRepository.GetAll();

            return alunos.Select(aluno => new AlunoResponse(aluno.Id, aluno.Nome, aluno.Cpf, aluno.Email)).ToList();
        }

        public AlunoResponse UpdateAluno(AlunoUpdateRequest alunoUpdateRequest)
        {
            var aluno = alunoRepository.GetById(alunoUpdateRequest.id);

            if (aluno == null)
            {
                throw new ApplicationException("Aluno não encontrado");
            }

            aluno.AtualizarAluno(alunoUpdateRequest.email);

            alunoRepository.Update(aluno);

            return new AlunoResponse(aluno.Id, aluno.Nome, aluno.Cpf, aluno.Email);
        }
    }
}
