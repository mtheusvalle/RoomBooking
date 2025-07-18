# RoomBooking

RoomBooking é um sistema para gerenciamento de reservas de salas, voltado para ambientes corporativos ou acadêmicos. O projeto oferece uma API robusta para criação, consulta e gerenciamento de salas e reservas, com validações de conflitos e notificações integradas. O objetivo é simplificar o controle de espaços compartilhados, evitando sobreposição de reservas e facilitando a administração.

## Instruções de execução

```bash
# Clone o repositório
git clone https://github.com/mtheusvalle/RoomBooking.git
cd RoomBooking

# Restaure os pacotes NuGet
dotnet restore

# Execute as migrations para criar o banco de dados
dotnet ef database update --project RoomBooking.Infrastructure/RoomBooking.Infrastructure.csproj --startup-project RoomBooking.Api/RoomBooking.Api.csproj

# Rode a aplicação
cd RoomBooking.Api
dotnet run
```

> Certifique-se de ter o [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado e o [EF Core CLI](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet) disponível.

## Técnicas e padrões utilizados

- **CQRS (Command Query Responsibility Segregation):** Separação clara entre comandos (escrita) e queries (leitura), melhorando a escalabilidade e manutenção do código. [Saiba mais](https://martinfowler.com/bliki/CQRS.html)
- **Validação com FluentValidation:** Uso de validadores para garantir integridade dos dados de entrada. [Documentação](https://docs.fluentvalidation.net/en/latest/)
- **Injeção de Dependência:** Configuração centralizada de serviços e repositórios, facilitando testes e extensibilidade. [Documentação Microsoft](https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection)
- **Middleware de tratamento global de exceções:** Centraliza o tratamento de erros, retornando respostas padronizadas para falhas na API. [Middleware](RoomBooking.Api/Middleware/ExceptionHandlingMiddleware.cs)
- **Entity Framework Core:** ORM para persistência de dados, com uso de migrations para versionamento do banco. [Documentação](https://learn.microsoft.com/pt-br/ef/core/)
- **AutoMapper:** Mapeamento automático entre entidades e DTOs, reduzindo boilerplate. [Documentação](https://automapper.org/)
- **Notificações externas via HttpClient:** Integração com serviços externos para envio de notificações. [HttpClient](RoomBooking.Infrastructure/HttpClients/NotificationClient.cs)

## Tecnologias e bibliotecas

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/)
- [AutoMapper](https://automapper.org/)
- [MediatR](https://github.com/jbogard/MediatR) (para CQRS)
- [xUnit](https://xunit.net/) (testes)
- [Swagger](https://swagger.io/) (documentação da API, se configurado)

## Estrutura do projeto

```
RoomBooking/
  RoomBooking.Api/
    Controllers/
    Middleware/
    Properties/
  RoomBooking.Application/
    Behaviors/
    Commands/
    DependencyInjection/
    DTOs/
    Events/
    Mappings/
    Properties/
    Queries/
    Services/
  RoomBooking.Domain/
    Entities/
    Exceptions/
    Interfaces/
    ValueObjects/
  RoomBooking.Infrastructure/
    DependencyInjection/
    HttpClients/
    Migrations/
    Persistence/
    Repositories/
  RoomBooking.Tests/
    Application/
    Domain/
```

### Descrições dos diretórios

- **RoomBooking.Api/**: API REST principal, com controllers, middlewares e configuração de inicialização.
- **RoomBooking.Application/**: Camada de aplicação, contendo comandos, queries, validações, mapeamentos e serviços de domínio.
- **RoomBooking.Domain/**: Entidades de negócio, interfaces de repositórios, exceções e value objects.
- **RoomBooking.Infrastructure/**: Implementação de persistência, repositórios, integrações externas e migrations do banco.
- **RoomBooking.Tests/**: Testes automatizados para domínio e aplicação.

## Documentação técnica

A documentação técnica detalhada está disponível em PDF:

[Documentação Técnica (PDF)](docs/RoomBooking_Documentacao.pdf) 