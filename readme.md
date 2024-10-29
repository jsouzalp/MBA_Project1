# **Blog - Aplicação de Blog Simples com MVC e API RESTful**

## **1. Apresentação**
Seja bem-vindo ao repositório do projeto **Blog**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo deste projeto é oferecer uma solução de de Blog com ações de CRUD de postagens realizados pelo Autor e comentários realizados pelos outros usuários da aplicação.
Inicialmente esta solução está desenvolvida em .Net 6

### **Autor**
- **Jairo Azevedo**

## **2. Proposta do Projeto**
O projeto consiste em:

- **Blog.Mvc:** Camada web de apresentação das informações.
- **Blog.Api:** WebAPI que oferece uma coleção de métodos para integração com outros tipos de dispositivos, desde que autenticado
- **Blog.Services:** Camada de serviços responsável por realizar consultas, validações e persistência dos dados coletados
- **Blog.Repositories:** Camada de repositório das informações que faz uso do ORM Entity Framework Core
- **Blog.AutoMapper:** Camada de mapeamento das entidades utilizadas no projeto
- **Blog.Translations:** Camada intermediária responsável por obter as traduções/recursos de mensagens apresentadas pelo sistema
- **Blog.Validations:** Camada responsável por realizar as validações na camada de serviços.
- **Autenticação e Autorização:** Implementação de controle de acesso.

## **3. Tecnologias Utilizadas**
- **Linguagem de Programação:** 
  - C# (.Net 6)
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
  - AutoMapper
  - FluentValidations
- **Banco de Dados:** 
  - SQLite
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

- docs/
- scripts/
- src/
  - .Settings/ - Arquivos de settings (compartilhados com os projetos de API e WEB)
  - Blog.Api/ 
  - Blog.AutoMapper/ 
  - Blog.Bases/
  - Blog.Entities/
  - Blog.Repositories/
  - Blog.Services/
  - Blog.Translations/
  - Blog.Validations/
  - Blog.Mvc/
- readme.md - Arquivo de Documentação do Projeto
- feedback.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de "ignores" do Git

## **5. Funcionalidades Implementadas**
- **CRUD para Posts e Comentários:** Permite criar, editar, visualizar e excluir posts e comentários.
- **API RESTful:** Exposição de endpoints para operações via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**
Para criação da estrutura de dados, é necessário localizar o arquivo *DatabaseSettings.json* e configurar nele a string de conexão. Após configurado, deve ser executado o migrations para criação da estrutura de tabelas:

## **Extensions do Visual Studio**
- Para visualização dos dados na base de dados criada (verifique a pasta ".\data") pelo Visual Studio, é necessária a instalação da extension "SQLite/SQL Server Compact Toolbox"
- Para um "deep-clean" dos arquivos temporários da solução, recomendo a instalação da extension "Open Command Line". Esta extension facilitará o uso do pacote ClearBinObj.cmd (veja em .helpers)

### **Pré-requisitos**
- .NET SDK 6.0 ou superior
- SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**
1. **Clone o Repositório:**
   - `git clone https://github.com/jsouzalp/MBA_Project1.git`

2. **Configuração do Banco de Dados:**
   - No arquivo `DatabaseSettings.json`, configure a string de conexão do SQLite para o database *ConnectionStringBlog*.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos
   - Instalar o dotnet ef (caso não tenha feito ainda):
     - dotnet tool install --global dotnet-ef
   - Para executar o migration:
     - dotnet ef migrations add InitialMigration --project .\Blog.Repositories --startup-project .\Blog.Mvc --context BlogDbContext
   - Para remover o migration:
     - dotnet ef migrations remove --project .\Blog.Repositories --startup-project .\Blog.Mvc --context BlogDbContext
   - Para ver o script que será gerado:
     - dotnet ef migrations script --no-build
   - Para sincronizar com o BD:
     - dotnet ef database update --project .\Blog.Mvc --context BlogDbContext

2. **Configuração do Banco de Dados de Autenticação (Identity):**
   - No arquivo `DatabaseSettings.json`, configure a string de conexão do SQLite para o database *ConnectionStringIdentity*.
   - Instalar o dotnet ef (caso não tenha feito ainda):
     - dotnet tool install --global dotnet-ef
   - Navegar para a pasta do projeto .\Blog.Mvc
   - Para executar o migration do *Identity*:
     - dotnet ef database update --project .\Blog.Mvc --context ApplicationDbContext

3. **Executar a Aplicação MVC:**
   - `cd src/Blog.Mvc/`
   - `dotnet run`
   - Acesse a aplicação em: http://localhost:5000

4. **Executar a API:**
   - `cd src/Blog.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5001/swagger

5. **Primeira Execução**
   Quando se tratar de uma primeira execução em ambiente de desenvolvimento, serão executados os Migrations e em seguida serão criados os usuários "jsouza.lp@gmail.com" e "cath.lp@gmail.com" com a password inicial "123" de forma automática já com algumas postagens e comentários para uma melhor experiência com a utilização da solução.
   
## **7. Instruções de Configuração**
- **JWT para API:** As chaves de configuração do JWT estão no `jwtSettings.json`.

## **8. Documentação da API**
A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em: http://localhost:5001/swagger

## **9. Avaliação**
- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.