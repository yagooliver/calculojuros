# Calculo juros composto

Qual o proposito da aplicação?
=====================
A aplicação consiste em 2 apis, uma que retorna a taxa de juros e a outra que consome essa api e realiza o calculo de juros compostos.

## Requerimentos

- Você precisará da versãoo 16.7.5 ou maior do Visual Studio 2019.
- .NET Core SDK 3.1
- SDK pode ser baixado em https://dot.net/core.

## Tecnologias implementadas:

- ASP.NET Core 3.1
- AZURE FUNCTIONS
- .NET Core Native DI
- FluentValidator
- MediatR
- Swagger UI
- Unit Tests(MSTest)

## Arquitetura:

- SOLID
- Domain Driven Design (Layers e Domain Model Pattern)
- Domain Notification
- CQRS

## Instruções

Você pode executar as functions pelo proprio visual studio ou acessar os links a seguir:

- https://calculojurosfunction.azurewebsites.net/api/swagger/ui
- https://taxajurosfunction.azurewebsites.net/api/swagger/ui

Apis:

- https://taxajurosfunction.azurewebsites.net/api/taxaJuros
- https://calculojurosfunction.azurewebsites.net/api/calculaJuros?valorInicial=100&meses=5
- https://calculojurosfunction.azurewebsites.net/api/showmethecode
