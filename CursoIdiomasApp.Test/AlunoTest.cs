using Bogus;
using Bogus.Extensions.Brazil;
using CursoIdiomasApp.Domain.Dtos.Requests;
using CursoIdiomasApp.Domain.Dtos.Responses;
using CursoIdiomasApp.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CursoIdiomasApp.Test
{
    public class AlunoTest
    {
        //objeto da biblioteca Bogus. O Faker serve para gerar dados falsos automaticamente, como nomes, números, endereços etc.
        private readonly Faker _faker = new Faker("pt_BR");//O "pt_BR" informa que os dados devem seguir a localização brasileira.

        //configuração que será utilizada quando o JSON retornado pela API for transformado em objetos C#
        private readonly JsonSerializerOptions _jsonOptions = new()// Esse new() é uma sintaxe abreviada do C#. Como o compilador já sabe que o tipo é JsonSerializerOptions, você não precisa escrever: new JsonSerializerOptions()
        {
            //JsonSerializerOptions É uma classe do System.Text.Json. Ela permite configurar como a serialização e desserialização JSON devem funcionar.Por exemplo: { "nome": "Notebook", "tipo": "Eletronico" }. pode ser convertido para: ProdutoResponse

            PropertyNameCaseInsensitive = true, //Significa: Ignore diferenças entre letras maiúsculas e minúsculas ao converter o JSON.
            Converters = { new JsonStringEnumConverter() } //Adiciona um conversor especial para enum.
        };


        //Esse método cria um HttpClient para testar sua API. O HttpClient permite realizar requisições HTTP
        private HttpClient CriarClient()
        {
            //O WebApplicationFactory pertence ao sistema de testes de integração do ASP.NET Core. Ele inicializa sua aplicação para os testes utilizando o Program da sua API.

            //.CreateClient() Cria um HttpClient conectado à aplicação criada pelo WebApplicationFactory.

            return new WebApplicationFactory<Program>().CreateClient();//basicamente significa: "Inicialize minha API para o teste e me dê um HttpClient que possa fazer requisições para ela."
        }

        private TurmaRequest CriarTurmaRequest()
        {
            return new TurmaRequest(
                _faker.Random.Int(1),
                _faker.PickRandom<TurmaEnum>()
                );
        }

        //cria os dados necessários para cadastrar um produto.
        private AlunoRequest CriarAlunoRequest(Guid turmaId)
        {
            return new AlunoRequest(
                _faker.Name.FullName(),
                _faker.Person.Cpf(includeFormatSymbols: false),
                _faker.Internet.Email(),
               turmaId
                );
        }

        //Esse método chama o endpoint da sua API para cadastrar um produto.
        private async Task<TurmaResponse> CriarTurma(HttpClient client) 
        {
            var request = CriarTurmaRequest();

            var response = await client.PostAsJsonAsync("/api/v1/turma/CriarTurma", request);//chamando a API; PostAsJsonAsync faz uma requisição: POST para: /api/v1/produto/criar enviando request como JSON.

            var content = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created, $"A API deveria criar turma, mas retornou {content}");//utilizando FluentAssertions. para verificar o status HTTP

            //var turma = await response.Content.ReadFromJsonAsync<TurmaResponse>(_jsonOptions);//pega o conteúdo retornado pela API e converte para: ProdutoResponse

            var turma = JsonSerializer.Deserialize<TurmaResponse>(content, _jsonOptions);

            turma.Should().NotBeNull();

            return turma!;
        }

        private async Task<AlunoResponse> CriarAluno(HttpClient client, Guid turmaId)
        {
            var request = CriarAlunoRequest(turmaId);

            var response = await client.PostAsJsonAsync("/api/v1/aluno/CriarAluno", request);

            //
            var content = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created, $"A API deveria criar Aluno, mas retornou {content}");

            var aluno = JsonSerializer.Deserialize<AlunoResponse>(content, _jsonOptions);
            //

            //response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            //var aluno = await response.Content.ReadFromJsonAsync<AlunoResponse>(_jsonOptions);

            aluno.Should().NotBeNull();

            return aluno!;
        }

        private async Task LimparDadosDoTeste(HttpClient client, Guid alunoId,Guid turmaId)
        {
            // 1. Buscar matrícula do aluno
            var matriculasResponse = await client.GetAsync(
                $"/api/v1/matricula/ListarPorAluno/{alunoId}"
            );

            if (matriculasResponse.IsSuccessStatusCode)
            {
                var matriculas =
                    await matriculasResponse.Content
                        .ReadFromJsonAsync<List<MatriculaResponse>>(_jsonOptions);

                if (matriculas is not null)
                {
                    foreach (var matricula in matriculas)
                    {
                        await client.DeleteAsync(
                            $"/api/v1/matricula/CancelarMatricula/{matricula.id}"
                        );
                    }
                }
            }

            // 2. Excluir aluno
            var alunoResponse = await client.DeleteAsync(
                $"/api/v1/aluno/DeletarAluno/{alunoId}"
            );

            alunoResponse.StatusCode.Should().Be(
                System.Net.HttpStatusCode.OK,
                "o aluno criado pelo teste deveria ser removido"
            );

            // 3. Excluir turma
            var turmaResponse = await client.DeleteAsync(
                $"/api/v1/turma/DeletarTurma/{turmaId}"
            );

            turmaResponse.StatusCode.Should().Be(
                System.Net.HttpStatusCode.OK,
                "a turma criada pelo teste deveria ser removida"
            );
        }

        [Fact(
            DisplayName = "POST api/v1/aluno/CriarAluno - Deve criar a aluno com sucesso")]
        public async Task DeveCriarAlunoComSucesso()
        {
            //Arrange
            var client = CriarClient();
            var turma = await CriarTurma(client);
            var aluno = await CriarAluno(client, turma.id);

            try
            {
                //Assert
                turma!.id.Should().NotBeEmpty();
                turma.numero.Should().Be(turma.numero);
                turma.anoLetivo.Should().Be(turma.anoLetivo);

                aluno!.id.Should().NotBeEmpty();
                aluno.nome.Should().Be(aluno.nome);
                aluno.cpf.Valor.Should().Be(aluno.cpf.Valor);
                aluno.email.Valor.Should().Be(aluno.email.Valor);
            }
            finally
            {
                await LimparDadosDoTeste(client, aluno.id, turma.id);
            }

        }

        [Fact(
            DisplayName = "GET api/v1/aluno/ListarAluno - Deve retorna aluno com sucesso")]
        public async Task DeveListarAlunoComSucesso()
        {
            //Arrange
            var client = CriarClient();
            var turma = await CriarTurma(client);
            var aluno = await CriarAluno(client, turma.id);

            try
            {
                //Assert
                aluno!.id.Should().NotBeEmpty();
                aluno.nome.Should().Be(aluno.nome);
                aluno.cpf.Valor.Should().Be(aluno.cpf.Valor);
                aluno.email.Valor.Should().Be(aluno.email.Valor);
            }
            finally
            {
                await LimparDadosDoTeste(client, aluno.id, turma.id);
            }

        }

        [Fact(
            DisplayName = "PATCH api/v1/aluno/AtualizarAluno - Deve atualizar aluno com sucesso")]
        public async Task DeveAtualizarAlunoComSucesso()
        {
            //Arrange
            var client = CriarClient();
            var turma = await CriarTurma(client);
            var aluno = await CriarAluno(client, turma.id);

            try
            {
                //Action

                var alunoAtualizado = new AlunoUpdateRequest(_faker.Internet.Email());

                var alunoResponse = await client.PatchAsJsonAsync($"/api/v1/aluno/AtualizarAluno/{aluno.id}", alunoAtualizado);
                //Assert
                var alunoData = await alunoResponse.Content.ReadFromJsonAsync<AlunoResponse>(_jsonOptions);

                alunoData!.id.Should().NotBeEmpty();
                alunoData.email.Valor.Should().Be(alunoAtualizado.email);
            }
            finally
            {
                await LimparDadosDoTeste(client, aluno.id, turma.id);
            }
        }

        [Fact(
            DisplayName = "DELETE api/v1/aluno/DeletarAluno - Deve deletar aluno com sucesso")]
        public async Task DeveDeletarAlunoComSucesso()
        {
            #region Arrange
            var client = CriarClient();
            var turma = await CriarTurma(client);
            var aluno = await CriarAluno(client, turma.id);
            #endregion

                //Action
                var matriculasResponse = await client.GetAsync($"/api/v1/matricula/ListarPorAluno/{aluno.id}");

                matriculasResponse.StatusCode.Should().Be(
                    System.Net.HttpStatusCode.OK
                );

                var matriculas = await matriculasResponse.Content.ReadFromJsonAsync<List<MatriculaResponse>>(_jsonOptions);
                //desserializar o JSON é necessário já que GetAsync() retorna um HttpResponseMessage (talvez seja necessário sempre desserializar a resposta da api quando você chama ela)

                matriculas.Should().NotBeNull();
                matriculas.Should().NotBeEmpty();

                var matriculaId = matriculas!.First().id;

                var cancelarResponse = await client.DeleteAsync($"/api/v1/matricula/CancelarMatricula/{matriculaId}");

                cancelarResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                var alunoResponse = await client.DeleteAsync($"/api/v1/aluno/DeletarAluno/{aluno.id}");

                var conteudo = await alunoResponse.Content.ReadAsStringAsync();

                alunoResponse.StatusCode.Should().Be(
                    System.Net.HttpStatusCode.OK,
                    $"A API deveria atualizar o aluno, mas retornou: {conteudo}"
                );

                //Assert
                var alunoData = await alunoResponse.Content.ReadFromJsonAsync<AlunoResponse>(_jsonOptions);

                alunoData?.id.Should().BeEmpty();

        }
    }
}
