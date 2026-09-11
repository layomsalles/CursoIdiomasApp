using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;

namespace CursoIdiomasApp.Domain.Interfaces.Services
{
    public interface IMatriculaService
    {
        MatriculaResponse CreateMatricula(MatriculaRequest matriculaRequest);
        List<Matricula> GetAllMatricula();
        MatriculaDeleteResponse DeleteMatricula();
        MatriculaResponse UpdateMatricula();
    }
}
