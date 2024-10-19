# **Blog - Aplicação de Blog Simples com MVC e API RESTful**

## **1. Apresentação**
Seja bem-vindo ao repositório do projeto **Blog**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo deste projeto é oferecer uma solução de de Blog com ações de CRUD de postagens realizados pelo Autor e comentários realizados pelos outros usuários da aplicação.
Inicialmente esta solução está desenvolvida em .Net Core 6

### **Autor**
- **Jairo Azevedo**

## **2. Proposta do Projeto**
O projeto consiste em:

- **ddd:** Camada web de apresentação das informações.
- **Blog.Api:** WebAPI que oferece uma coleção de métodos para integração com outros tipos de dispositivos, desde que autenticado
- **Blog.Services:** Camada de serviços responsável por realizar consultas, validações e persistência dos dados coletados
- **Blog.Repositories:** Camada de repositório das informações que faz uso do ORM Entity Framework Core
- **Blog.AutoMapper:** Camada de mapeamento 
- **ddd:** 
- **ddd:** 
- **ddd:** 
- **ddd:** 
- **ddd:** 

- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** 
  - C# (.Net Core 6)
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
  - AutoMapper
  - FluentValidations
- **Banco de Dados:** 
  - SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** 
  - Swagger (Apenas em ambiente de desenvolvimento)

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:


- src/
  - Blog.Web/ - Projeto MVC
  - Blog.Api/ - API RESTful
  - Blog.Data/ - Modelos de Dados e Configuração do EF Core
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **Autenticação e Autorização:** Diferenciação entre usuários comuns e administradores.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/seu-usuario/nome-do-repositorio.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos

3. **Executar a Aplicação MVC:**
   - `cd src/Blog.Mvc/`
   - `dotnet run`
   - Acesse a aplicação em: http://localhost:5000

4. **Executar a API:**
   - `cd src/Blog.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5001/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5001/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.



Instalar o dotnet ef:
dotnet tool install --global dotnet-ef

Para executar o migration:
dotnet ef migrations add 0001_InitialMigration --project ..\Blog.Repositories --startup-project ..\Blog.Api --no-build

Para remover o migration:
dotnet ef migrations remove --project ..\Blog.Repositories --startup-project ..\Blog.Api --no-build

Para ver o script que será gerado:
dotnet ef migrations script --no-build

Para sincronizar com o BD
dotnet ef database update
