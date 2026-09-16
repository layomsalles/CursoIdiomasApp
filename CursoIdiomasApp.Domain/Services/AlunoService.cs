using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Exceptions;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;
using CursoIdiomasApp.Domain.Value_Objects;

namespace CursoIdiomasApp.Domain.Services
{
    public class AlunoService(IAlunoRepository alunoRepository, 
        ITurmaRepository turmaRepository, 
        IMatriculaRepository matriculaRepository) : IAlunoService
    {
        public AlunoResponse CreateAluno(AlunoRequest alunoRequest)
        {
            var turma = turmaRepository.GetByIdComMatriculas(alunoRequest.Turmaid);
            //passando os valores da request para os Value Object Cpf e Email
            var cpf = new Cpf(alunoRequest.cpf);
            var email = new Email(alunoRequest.email);

            if (alunoRepository.ExistePorCpf(cpf))
            {
                throw new DomainException("CPF de aluno já cadastrado");
            }

            if (alunoRepository.ExistePorEmail(email))
            {
                throw new DomainException("Email de Aluno já cadastrado");
            }

            if (turma == null)
            {
                throw new DomainException("Turma não encontrada");
            }

            var aluno = new Aluno
            (
                alunoRequest.nome,
                cpf,
                email
            );

            // Primeiro salva o aluno
            alunoRepository.Create(aluno);

            // A entidade Turma aplica as regras de negócio
            var matricula = turma.MatricularAluno(aluno);

            // Salva explicitamente a nova matrícula
            matriculaRepository.Create(matricula);

            return new AlunoResponse(aluno.Id, aluno.Nome, aluno.Cpf, aluno.Email);
        }

        public AlunoDeleteResponse DeleteAluno(Guid id)
        {
            var aluno = alunoRepository.GetById(id);

            if (aluno == null) {
                throw new ApplicationException("Aluno não encontrado");
            }

            var matriculas = matriculaRepository.GetByAluno(id);

            if (matriculas.Any())
            {
                throw new DomainException(
                    "O aluno não pode ser excluído porque está matriculado em uma turma"
                );
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
