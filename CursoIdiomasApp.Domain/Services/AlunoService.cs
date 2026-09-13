using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;
using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Domain.Services
{
    public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
    {
        public AlunoResponse CreateAluno(AlunoRequest alunoRequest)
        {
            //passando os valores da request para os Value Object Cpf e Email
            var cpf = new Cpf(alunoRequest.cpf);
            var email = new Email(alunoRequest.email);

            var aluno = new Aluno
            (
                alunoRequest.nome,
                cpf,
                email
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

        public AlunoResponse UpdateAluno(Guid id, AlunoUpdateRequest alunoUpdateRequest)
        {
            var aluno = alunoRepository.GetById(id);

            if (aluno == null)
            {
                throw new ApplicationException("Aluno não encontrado");
            }

            var Aluno = new Email(alunoUpdateRequest.email);

            aluno.AtualizarAluno(Aluno);

            alunoRepository.Update(aluno);

            return new AlunoResponse(aluno.Id, aluno.Nome, aluno.Cpf, aluno.Email);
        }
    }
}
