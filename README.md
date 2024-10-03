![Código Certo Coders](https://utfs.io/f/3b2340e8-5523-4aca-a549-0688fd07450e-j4edu.jfif)

# 📚 Trilha Inicial BackEnd Jr
Este projeto tem como objetivo desenvolver uma API RESTful para gerenciamento de tarefas, proporcionando funcionalidades de CRUD (Create, Read, Update, Delete) de tarefas, autenticação de usuários e armazenamento dos dados em um banco de dados, utilizando o Frameworok .NET e ASP.NET Core juntamente com a linguagem C#, EntityFramework, SQLite para o banco de dados.

## PRÉ REQUISITOS ##

.NET SDK - Necessário fazer a instalação do .NET em seu S.O
``Utilizei e versão 8.0.304``

Para configurar e rodar esse projeto deve ser instalado o SQLite em seu S.O.
``Versão que usei foi a 3.46.1``

## CONFIGURAÇÕES ##

``Configurar o SQLite``

1 - Faça o  donwload em: https://www.sqlite.org/ para sua versão de bits do S.O.
2 - Instale em seus disco local C:
3- Configure nas variáveis de ambiente, a variável PATH adicionando uma nova variável com o caminho do executável do sqlite que foi salvo no C:

# String de conexão no appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Data Source=C:\\SQLite\\mydatabase.db"
}

# Criar as Migrations

Como as migrations não estão incluídas no repositório, você precisará gerá-las manualmente. Execute o seguinte comando para criar a migration inicial:

   bash: ``dotnet ef migrations add InitialCreate``

Esse comando criará as migrations necessárias para gerar as tabelas no banco de dados SQLite.

Após criar as migrations, aplique-as ao banco de dados para gerar as tabelas. Use o seguinte comando: ``dotnet ef database update``

## Instalar a autenticação JWT no projeto

1 - Instalar o pacote: ``Microsoft.AspNetCore.Authentication.JwtBearer``, este pacote adiciona suporte para autenticação JWT no ASP.NET Core.

2 - Instalar os pacotes do ASP.NET Identity: ``Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore``

## Configuração da Autenticação JWT

Este projeto utiliza ``JWT (JSON Web Token)`` para autenticação.

### Geração da Chave Secreta JWT

1. Gere uma chave secreta segura. Aqui estão alguns métodos sugeridos:
   - Usando OpenSSL:
     ```bash
     openssl rand -base64 32
     ```
   - Usando PowerShell (Windows):
     ```powershell
     [Convert]::ToBase64String((1..32 | ForEach-Object {Get-Random -Maximum 256}))
     ```

2. Adicione a chave gerada ao arquivo **appsettings.json** na seção **Jwt**:
   {
     "Jwt": {
       "Key": "SUA_CHAVE_SECRETA_AQUI",
       "Issuer": "https://localhost:5001",
       "Audience": "https://localhost:5001",
       "ExpireMinutes": 60
     }
   }

## API Documentation
``Authentication Endpoints``

1. ``Register a new user``

URL: POST /api/auth/register
Description: Registra um novo usuário no sistema.

Headers:
Content-Type: application/json

Request Body:
{
  "userName": "testuser",
  "email": "testuser@example.com",
  "password": "YourStrongPassword123!"
}

Response:
201 Created: Se tudo ocorrer bem, retornara 201.
400 Bad Request: Se tiver erro na validação, como por exemplo ('User already registered').

2. ``Login de usuário``, para se logar no sistema e acessar os outros end-points é preciso fazer o login para receber o token de acesso

URL: POST api/auth/login

Headers:
Content-Type: application/json

Request-Body:
{
   "email": "testuser",
   "password":"YourStrongPassword123!"
}

Response:
200 OK: Se tudo ocorrer bem, retornará 200.
Token: o token será retornado para posterior usar na api de tarefas
400 Bad Request: Se tiver erro na validação, como ('Invalid Password').

3. ``Criação de Tarefas``, aqui é necessário estar logado para usar o token

URL: POST api/task

Headers:
Content-Type: application/json
Authorization: Bearer SeuTokenAqui

POST: Request-Body:
{
   "title": "Titulo da tarefa",
   "description": "descrição",
   "status: aqui pode preencher de 0 a 7
}

Response:
201 Created: Se ocorrer bem, retornará 201.
500 Internal Error: Se ocorrer algum erro em salvar a tarefa, retornará 500 com a mensagem do erro

4. ``Consulta na tarefa``

URL: GET api/task/iddatarefa

Headers:
Content-Type: application/json
Authorization: Bearer SeuTokenAqui

Response:
200 OK: Se ocorrer bem, retornará 200 com a tarefa no JSON.
500 Internal Error: Se ocorrer algum erro em salvar a tarefa, retornará 500 com a mensagem do erro

5. ``Editando Tarefa``

URL: POST api/task/iddatarefa

Headers:
Content-Type: application/json
Authorization: Bearer SeuTokenAqui

Request-body:
{
    "title": "alterando tarefa",
    "description": "outra tarefa",
    "status": 3
}

Response:
200 OK: Se ocorrer bem, retornará 200 com a tarefa no JSON.
500 Internal Error: Se ocorrer algum erro em salvar a tarefa, retornará 500 com a mensagem do erro

6. ``Excluindo Tarefa``

URL: DELETE api/task/iddatarefa

Headers:
Content-Type: application/json
Authorization: Bearer SeuTokenAqui

Response:
204 No Content: Se ocorrer bem, retornará 204.
500 Internal Error: Se ocorrer algum erro em salvar a tarefa, retornará 500 com a mensagem do erro

7. ``Associando vários usuários a 1 tarefa``

URL: POST api/task/assign-users

Headers:
Content-Type: application/json
Authorization: Bearer SeuTokenAqui

Request-body:
{
    "taskId": "id da tarefa",
    "userIds": [
        "3db9f788-841c-46e8-bdb8-d2c58280a33a",
        "79d2fb38-2db4-460d-8024-a875ab122232"
    ]
}

Response:
200 OK: Se ocorrer bem, retornará 200 com a tarefa no JSON.
500 Internal Error: Se ocorrer algum erro em salvar a tarefa, retornará 500 com a mensagem do erro

## Objetivos:
- Criar uma API que permita CRUD (Create, Read, Update, Delete) de tarefas.
- Implementar autenticação de usuários.
- Utilizar um banco de dados SQLite para armazenar as tarefas.
- Documentar todo o processo e apresentar as conclusões.

## Requisitos Funcionais:
- Criar Tarefa: Endpoint para criar uma nova tarefa.
- Listar Tarefas: Endpoint para listar todas as tarefas.
- Atualizar Tarefa: Endpoint para atualizar uma tarefa existente.
- Deletar Tarefa: Endpoint para deletar uma tarefa existente.

## Autenticação de Usuários:
- Registro de Usuário: Endpoint para registrar um novo usuário.
- Login de Usuário: Endpoint para autenticar um usuário e gerar um token JWT.
- Proteção de Rotas: Garantir que apenas usuários autenticados possam acessar os endpoints de tarefas.

## Banco de Dados:
- Utilizar SQLite como banco de dados para armazenar informações de usuários e tarefas.

   #### Estrutura do Projeto:
   ```plaintext
   project-root/
   │
   ├── src/
   │   ├── controllers/
   │   ├── models/
   │   ├── routes/
   │   ├── middlewares/
   │   ├── database/
   │   └── app.js
   │
   ├── .env
   ├── .gitignore
   ├── README.md
   └── package.json
   ```

