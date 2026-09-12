using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Exceptions;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;

namespace CursoIdiomasApp.Domain.Services
{
    public class MatriculaService(IMatriculaRepository matriculaRepository) : IMatriculaService
    {
        public MatriculaDeleteResponse CancelarMatricula(Guid Id)
        {
            var matricula = matriculaRepository.GetById(Id);

            if(matricula == null)
            {
                throw new DomainException("Matricula não encontrada");
            }

            matriculaRepository.Delete(matricula);

            return new MatriculaDeleteResponse("Matricula deletada com sucesso");
        }

        public List<MatriculaResponse> ConsultarPorAluno(Guid AlunoId)
        {
            var matriculaAluno = matriculaRepository.GetByAluno(AlunoId);

            return matriculaAluno.Select(aluno => new MatriculaResponse(aluno.Id, aluno.AlunoId, aluno.TurmaId, aluno.DataMatricula)).ToList();
        }

        public List<MatriculaResponse> ConsultarPorTurma(Guid TurmaId)
        {
            var matriculaTurma = matriculaRepository.GetByTurma(TurmaId);

            return matriculaTurma.Select(turma => new MatriculaResponse(turma.Id, turma.AlunoId, turma.TurmaId, turma.DataMatricula)).ToList();
        }

        public MatriculaResponse CreateMatricula(MatriculaRequest matriculaRequest)
        {
            if(matriculaRepository.Exists(matriculaRequest.AlunoId, matriculaRequest.TurmaId))
            {
                throw new DomainException("Aluno já está matriculado nessa turma");
            }

            if(matriculaRepository.CountByTurma(matriculaRequest.TurmaId) >= 5)
            {
                throw new DomainException("Turma não poder ter mais que 5 alunos");
            }

            var matricula = new Matricula(matriculaRequest.AlunoId, matriculaRequest.TurmaId);

            matriculaRepository.Create(matricula);

            return new MatriculaResponse(matricula.Id, matricula.AlunoId, matricula.TurmaId, matricula.DataMatricula);
        }
    }
}
