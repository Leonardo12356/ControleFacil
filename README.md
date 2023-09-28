# ControleFacil API

A API ControleFacil é uma aplicação desenvolvida em .NET 7 que oferece recursos de controle de usuário, gestão de pagamentos e outras funcionalidades relacionadas ao gerenciamento financeiro. Ela utiliza Entity Framework para interagir com o banco de dados, AutoMapper para mapeamento de objetos e autenticação baseada em token.

## Funcionalidades Principais

- **Controle de Usuário**: A API permite o registro, autenticação e gerenciamento de usuários.

- **Gestão de Pagamentos**: Você pode criar, consultar, atualizar e excluir pagamentos e recebimentos. As entidades principais incluem:
  - Pagamento: Representa um pagamento a ser efetuado.
  - Recebimento: Representa um valor a ser recebido.
  - Natureza de Lançamento: Define a natureza do lançamento (por exemplo, despesa ou receita).
  - Título: Representa uma descrição ou título associado a um pagamento ou recebimento.


### Pré-requisitos

- .NET 7 SDK instalado (https://dotnet.microsoft.com/download/dotnet/7.0)
- Banco de dados PostgreSQL instalado e configurado (https://www.postgresql.org/)

- Configure o banco de dados: Atualize a string de conexão e senha do banco de dados PostgreSQL no arquivo appsettings.json.

Aplique as migrações do Entity Framework acessando cd src/ControleFacil.Api:
em seguida execute os comandos no terminal

dotnet ef migrations add InitialCreate
dotnet ef database update

para executar o projeto
dotnet watch run




