# Teste Técnico - Consultor Soluções IV (.Net Core) - Capgemini
> Parte 1 - Montar uma API usando C# preferencialmente ASP.NET Core versão 3.1 ou superior com os seguintes métodos:

## Tecnologias usadas no projeto:
* ASP.NET Core 3.1
* Entity Framework Core 3.1
* .NET Core Native DI
* FluentValidator
* Versionamento de APIs
* MediatR
* Swagger UI 

## Arquitetura:

* Arquitetura aplicada nos projetos, tem como preocupação na separação de responsabilidades e código limpo.
* Domain Driven Design (Layers and Domain Model Pattern)
* Domain Events
* Domain Notification
* Domain Validations
* CQRS (Imediate Consistency)
* Unit of Work
* Repository

------------

## Banco de dados
* Instale o banco de dados SQLServer ou utilize a versão MSSQlocalDB. 
* Edita o arquivo script.sql, que está localizado em 'api-rest-importador/server/scripts/', após as alterações execute o script
 
------------

## Executando o Projeto
**Antes de qualquer coisa, prepare o ambiente de desenvolvimento** 
* Faça um clone do projeto
* Acesse o mesmo
* Altere o arquivo appsettings.json, as informações da conexão do banco de dados.
 
**Após a finalização do build, o link para acesso do swagger  da [APIRestImportador](https://localhost:44369/swagger/index.html).**

## Realizando envio de arquivo .xlsx via postman
* Para o envio do arquivo, na seção do Headers, inclui a instrução: "key" utilize "Content-Type" e no "value" "multipart/form-data", na seção Body, inclua "key" utilize "file" e no "value" selecione o tipo File e escolha o arquivo.



