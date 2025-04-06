# DevExpert.Marketplace - Gestão de Mini Loja Virtual com Cadastro de Produtos e Categorias

## 1. Apresentação

Bem-vindo ao repositório do projeto **DevExpert.Marketplace**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**. O objetivo principal é desenvolver uma aplicação de marketplace simplificada que permite aos usuários criar, editar, visualizar e excluir produtos e categorias, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful.

O sistema simula uma área administrativa de um e-commerce, onde cada usuário autenticado é considerado um vendedor e pode gerenciar seus próprios produtos.

### Autor(es)
- Edson Santos

## 2. Proposta do Projeto

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com o marketplace.
- **API RESTful:** Exposição dos recursos do marketplace para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Controle de acesso com ASP.NET Core Identity e JWT para a API.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de Entity Framework Core.

## 3. Tecnologias Utilizadas

- **Linguagem de Programação:** C#
- **Frameworks:**
    - ASP.NET Core MVC
    - ASP.NET Core Web API
    - Entity Framework Core
- **Banco de Dados:** SQL Server (Produção) / SQLite (Desenvolvimento)
- **Autenticação e Autorização:**
    - ASP.NET Core Identity
    - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
    - Razor Pages/Views
    - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## 4. Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma:

```
DevExpert.Marketplace/
│
├── src/
│   ├── DevExpert.Marketplace.Api - API RESTful
│   ├── DevExpert.Marketplace.App - Aplicação Principal
│   ├── DevExpert.Marketplace.Application - Camada de Aplicação
│   ├── DevExpert.Marketplace.Business - Regras de Negócio
│   ├── DevExpert.Marketplace.Data - Modelos de Dados e Configuração do EF Core
│   ├── DevExpert.Marketplace.IoC - Injeção de Dependências
│   ├── DevExpert.Marketplace.Shared - Recursos Compartilhados
│
├── README.md - Arquivo de Documentação do Projeto
├── FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
├── .gitignore - Arquivo de Ignoração do Git
```

## 5. Funcionalidades Implementadas

- CRUD para Produtos e Categorias: Permite criar, editar, visualizar e excluir produtos e categorias.
- Autenticação e Autorização: Controle de acesso diferenciado entre usuários comuns e administradores.
- API RESTful: Exposição de endpoints para operações CRUD via API.
- Documentação da API: Documentação automática dos endpoints da API utilizando Swagger.

## 6. Como Executar o Projeto

### Pré-requisitos
- .NET SDK 9.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### Passos para Execução
1. Clone o Repositório:
```
git clone https://github.com/edsonsantos-dev/DevExpert.Marketplace.git
cd DevExpert.Marketplace
```

2. Configuração do Banco de Dados:
- No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
- Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.

3. Executar a Aplicação MVC:
```
cd src/DevExpert.Marketplace.App/
dotnet run
```
Acesse a aplicação em: http://localhost:5000

4. Executar a API:
```
cd src/DevExpert.Marketplace.Api/
dotnet run
```
Acesse a documentação da API em: http://localhost:5001/swagger

## 7. Instruções de Configuração e Execução por Ambiente

### Ambiente de Desenvolvimento
Para rodar a aplicação em ambiente de desenvolvimento (usualmente com banco de dados SQLite):

```bash
ASPNETCORE_ENVIRONMENT=Development dotnet run
```
Ou utilizando um perfil de lançamento:
```bash
dotnet run --launch-profile "Development"
```

### Ambiente de Produção
Para rodar a aplicação em ambiente de produção (usualmente com banco de dados SQL Server):

```bash
ASPNETCORE_ENVIRONMENT=Production dotnet run
```
Ou utilizando um perfil de lançamento:
```bash
dotnet run --launch-profile "Production"
```

### Detalhes de Configuração
- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido à configuração do Seed de dados.

## 8. Documentação da API

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:
```
http://localhost:5001/swagger
```

## 9. Avaliação

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas.
- Para feedbacks ou dúvidas utilize o recurso de Issues.
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.

