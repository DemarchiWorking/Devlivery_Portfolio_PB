namespace TestProject1
{
    public class UnitTest1
    {
        public class ProjetoControllerTests
        {
            private readonly ProjetoControllerTests _projetoController;

            public ProjetoControllerTests()
            {
                // Inicialize o controller (pode ser necessário configurar dependências)
                _projetoController = new ProjetoControllerTests();
            }

            [Fact]
            public void Test_CadastrarProjeto_Valido()
            {
                var projeto = new CadastroProjetoModel
                {
                };

                var result = _projetoController.CadastrarProjeto(projeto);

                Assert.True(result is OkResult);
            }

            [Fact]
            public void Test_CadastrarProjeto_Invalido()
            {
                // Arrange
                var projeto = new CadastroProjetoModel
                {
                    // Preencha os campos do projeto com dados inválidos
                    // ...
                };

                // Act
                var result = _projetoController.CadastrarProjeto(projeto);

                // Assert
                Assert.True(result is BadRequestResult);
            }

            [Fact]
            public void Test_CadastrarProjeto_Excecao()
            {
                // Arrange
                var projeto = new CadastroProjetoModel
                {
                    // Preencha os campos do projeto com dados válidos
                    // ...
                };

                // Simule uma exceção (por exemplo, erro de banco de dados)
                // ...

                // Act
                var result = _projetoController.CadastrarProjeto(projeto);

                // Assert
                Assert.True(result is ObjectResult objResult && objResult.StatusCode == 500);
            }
        }
    }