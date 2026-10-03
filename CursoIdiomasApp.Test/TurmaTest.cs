using Bogus;
using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Entities;
using CursoIdiomasApp.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CursoIdiomasApp.Test
{
    public class TurmaTest
    {
        private readonly Faker _faker = new Faker();

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,

            Converters = { new JsonStringEnumConverter() }
        };

        private HttpClient CriarClient()
        {
            return new WebApplicationFactory<Program>().CreateClient();
        }

        private int GerarNumeroTurma()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }

        private TurmaRequest CriarTurmaRequest()
        {
            return new TurmaRequest(
                GerarNumeroTurma(),
                _faker.PickRandom<TurmaEnum>()
                );
        }

        private async Task<TurmaResponse> CriarTurma(HttpClient client, TurmaRequest turmaRequest) 
        {
            var request = turmaRequest;

            var response = await client.PostAsJsonAsync("api/v1/turma/CriarTurma", request);

            var content = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created, $"A API deveria criar turma, mas retornou {content}");

            var turma = JsonSerializer.Deserialize<TurmaResponse>(content,_jsonOptions);

            turma.Should().NotBeNull();

            return turma!;
        }

        private async Task LimparTurma(HttpClient client, Guid turmaId)
        {
            var response = await client.DeleteAsync(
                $"/api/v1/turma/DeletarTurma/{turmaId}"
            );

            response.StatusCode.Should().Be(
                System.Net.HttpStatusCode.OK,
                "a turma criada pelo teste deveria ser removida"
            );
        }

        [Fact(DisplayName = "POST api/v1/turma/CriarTurma - Deve criar turma com sucesso")]
        public async Task DeveCriarTurmaComSucesso()
        {
            //Arrange
            var client = CriarClient();
            var turmaRequest = CriarTurmaRequest();
            var turma = await CriarTurma(client, turmaRequest);

            try
            {
                //Action

                //Assert
                var turmaData = turma;

                turmaData.id.Should().NotBeEmpty();
                turma.numero.Should().Be(turmaRequest.Numero);
                turma.anoLetivo.Should().Be(turmaRequest.AnoLetivo);
            }
            finally
            {
                await LimparTurma(client, turma.id);
            }
        }

        [Fact(DisplayName = "GET api/v1/turma/ListarTurma - Deve listar turma com sucesso")]
        public async Task DeveListarTurmaComSucesso()
        {
            //Arrange
            var client = CriarClient();
            var turmaRequest = CriarTurmaRequest();
            var turma = await CriarTurma(client, turmaRequest);

            try
            {

                //Action

                var listarTurmaResponse = await client.GetAsync("/api/v1/turma/ListarTurma");
                listarTurmaResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                //Assert
                var turmaData = await listarTurmaResponse.Content.ReadFromJsonAsync<List<TurmaResponse>>(_jsonOptions);


                turmaData.Should().NotBeNull();
                turmaData.Should().NotBeEmpty();

                turmaData.Should().Contain(turma =>
                    turma.id == turma.id &&
                    turma.numero == turmaRequest.Numero &&
                    turma.anoLetivo == turmaRequest.AnoLetivo
                );
            }
            finally
            {
                await LimparTurma(client, turma.id);
            }

        }

        [Fact(DisplayName = "DELETE api/v1/turma/DeletarTurma - Deve deletar turma com sucesso")]
        public async Task DeveDeletarTurmaComSucesso() 
        {
            //Arrange
            var client = CriarClient();
            var turmaRequest = CriarTurmaRequest();
            

            //Action
            var turma = await CriarTurma(client, turmaRequest);

            var listarTurmaResponse = await client.GetAsync("/api/v1/turma/ListarTurma");

            var turmaResponse = await client.DeleteAsync($"/api/v1/turma/DeletarTurma/{turma.id}");

            //Assert
            turmaResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact(DisplayName = "PATCH api/v1/turma/AtualizarTurma - Deve atualizar turma com sucesso")]
        public async Task DeveAtualizarTurmaComSucesso() 
        {
            //Arrange
            var client = CriarClient();
            var turmaRequest = CriarTurmaRequest();
            var turma = await CriarTurma(client, turmaRequest);

            try
            {
                //Action
                var turmaAtualizada = new TurmaRequest(
                    GerarNumeroTurma(),
                    _faker.PickRandom<TurmaEnum>()
                    );

                var response = await client.PatchAsJsonAsync($"/api/v1/turma/AtualizarTurma/{turma.id}", turmaAtualizada);
                var content = await response.Content.ReadAsStringAsync();
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK, $"A API deveria atualizar a turma, mas retornou: {content}");

                //Assert
                var turmaData = await response.Content.ReadFromJsonAsync<TurmaResponse>(_jsonOptions);


                turmaData.Should().NotBeNull();
                turmaData!.id.Should().Be(turma.id);
                turmaData.numero.Should().Be(turmaAtualizada.Numero);
                turmaData.anoLetivo.Should().Be(turmaAtualizada.AnoLetivo);
            }
            finally
            {
                await LimparTurma(client, turma.id);
            }

        }
    }
}
