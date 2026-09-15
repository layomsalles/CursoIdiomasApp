using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Exceptions;
using CursoIdiomasApp.Domain.Interfaces.Repositories;
using CursoIdiomasApp.Domain.Interfaces.Services;

namespace CursoIdiomasApp.Domain.Services
{
    public class MatriculaService(IMatriculaRepository matriculaRepository, ITurmaRepository turmaRepository, IAlunoRepository alunoRepository) : IMatriculaService
    {
        public MatriculaDeleteResponse CancelarMatricula(Guid Id)
        {
            var matricula = matriculaRepository.GetById(Id);

            if(matricula == null)
            {
                throw new DomainException("Matricula não encontrada");
            }

            var turma = turmaRepository.GetByIdComMatriculas(matricula.TurmaId);

            if (turma == null)
            {
                throw new DomainException("Turma não encontrada");
            }

            turma.CancelarMatricula(Id);

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
            var aluno = alunoRepository.GetById(matriculaRequest.AlunoId);

            if(aluno == null)
            {
                throw new DomainException("Aluno não encontrado");
            }

            var turma = turmaRepository.GetByIdComMatriculas(matriculaRequest.TurmaId);

            if (turma == null)
            {
                throw new DomainException("Turma não encontrado");
            }

            //Como turma tem acesso à MatricularAluno() ?
            var matricula = turma.MatricularAluno(aluno);

            matriculaRepository.Create(matricula);

            return new MatriculaResponse(matricula.Id, matricula.AlunoId, matricula.TurmaId, matricula.DataMatricula);
        }
    }
}
