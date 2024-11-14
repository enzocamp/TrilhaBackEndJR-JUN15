# 📚 Trilha Inicial BackEnd Jr
Este projeto tem como objetivo desenvolver uma API RESTful para gerenciamento de tarefas, proporcionando funcionalidades de CRUD (Create, Read, Update, Delete) de tarefas, autenticação de usuários e armazenamento dos dados em um banco de dados, utilizando o Frameworok .NET e ASP.NET Core juntamente com a linguagem C#, EntityFramework, SQLite para o banco de dados.

## PRÉ REQUISITOS ##

.NET SDK - Necessário fazer a instalação do .NET em seu S.O
``Utilizei e versão 8.0.304``

Para configurar e rodar esse projeto deve ser instalado o SQLite em seu S.O.
``Versão que usei foi a 3.46.1``

## CONFIGURAÇÕES PARA TESTAR A API ##

URL Base: ``https://api-task-mvc.onrender.com``

## Endpoints Principais ##

## Registro/Login ##
``Autenticação``
Esses endpoints são usados para registrar um novo usuário e fazer login.

Registrar um novo usuário
Endpoint: POST url base + /api/auth/register

Descrição: Cria uma nova conta de usuário.

Exemplo de Corpo da Requisição (JSON):
{
  "email": "example@example.com",
  "password": "YourPassword123",
  "confirmPassword": "YourPassword123"
}

Resposta de Sucesso (201 Created):
{
  "message": "User registered successfully"
}

``Login de Usuário``
Endpoint: POST url base + /api/auth/login

Descrição: Faz login na conta do usuário e retorna um token JWT para autenticação.

Exemplo de Corpo da Requisição (JSON):
{
  "email": "example@example.com",
  "password": "YourPassword123"
}

Resposta de Sucesso (200 OK):
{
  "token": "jwt_token_here"
}

## Gerenciamento de Tarefas ##
Esses endpoints permitem a criação, atualização, listagem e exclusão de tarefas. Todos os endpoints de tarefas exigem um token JWT no cabeçalho de autorização.

``Criar uma Tarefa``
Endpoint: POST url base + /api/task

Descrição: Cria uma nova tarefa.

Cabeçalho de Autorização:
Authorization: Bearer jwt_token_here

Status esperado: Created = 0,WaitingForActivation = 1,WaitingToRun = 2,Running = 3,aitingForChildrenToComplete = 4,RanToCompletion = 5,Canceled = 6,Faulted = 7

Exemplo de Corpo da Requisição (JSON):
{
  "title": "Estudar .NET",
  "description": "Estudar para a certificação .NET",
  "status": "0"
}

Resposta de Sucesso (201 Created):
{
    "id": "1e655a13-b780-4a62-9e6f-f246fee188e6",
    "title": "Estudar .NET",
    "description": "Estudar para a certificação .NET",
    "status": 0,
    "taskUsers": []
}

``Listar Tarefas``
Endpoint: GET url base + /api/task

Descrição: Retorna uma lista de todas as tarefas do usuário.

Cabeçalho de Autorização:
Authorization: Bearer jwt_token_here

Resposta de Sucesso (200 OK):
    {
        "id": "1e655a13-b780-4a62-9e6f-f246fee188e6",
        "title": "Estudar .NET",
        "description": "Estudar para a certificação .NET",
        "status": 0,
        "taskUsers": []
    },
    

``Atualizar uma Tarefa``
Endpoint: PUT url base + /api/task/{id}

Descrição: Atualiza os detalhes de uma tarefa existente.

Cabeçalho de Autorização:
Authorization: Bearer jwt_token_here

Exemplo de Corpo da Requisição (JSON):
{
  "title": "Estudar ASP.NET Core",
  "description": "Estudar o framework ASP.NET Core para aprimorar conhecimentos",
  "dueDate": "2024-11-20T00:00:00",
  "isCompleted": true
}

Resposta de Sucesso (200 OK):
{
    "id": "1e655a13-b780-4a62-9e6f-f246fee188e6",
    "title": "Estudar ASP.NET Core",
    "description": "Estudar o framework ASP.NET Core para aprimorar conhecimentos",
    "status": 2,
    "taskUsers": []
}

``Deletar uma Tarefa``
Endpoint: DELETE url base + /api/task/{id}

Descrição: Exclui uma tarefa específica.

Cabeçalho de Autorização:
Authorization: Bearer jwt_token_here

Resposta de Sucesso (204 No Content):
{
  "message": "Task deleted successfully"
}

``Assign Users to Task``
Endpoint: POST url base + /api/task/{taskId}/assign-users

Descrição: Associar usuários a uma tarefa

Cabeçalho de Autorização:
Authorization: Bearer jwt_token_here
Content-Type: application/json

Parâmetros da URL
taskId: O ID da tarefa à qual você deseja associar os usuários.

Corpo da Requisição (JSON)
Envie uma lista de IDs de usuários que você deseja associar à tarefa.
{
  "userIds": ["userId1", "userId2", "userId3"]
}

Respostas
200 OK: Usuários associados com sucesso.
{
  "message": "Users assigned to task successfully"
}

``Get Tasks with Assigned Users``
Descrição: Esse endpoint permite visualizar as tarefas com os usuários que foram atribuídos a elas.

Endpoint: POST url base + /api/task/tasks-with-users
Método: GET
Autenticação: Bearer jwt_token_here

Exemplo de Resposta (200 OK)
[
  {
    "taskId": "a12345",
    "taskTitle": "Task Example 1",
    "taskUsers": [
      {
        "userId": "b12345",
        "userName": "User1"
      },
      {
        "userId": "c67890",
        "userName": "User2"
      }
    ]
  }
]

## CONFIGURAÇÕES PARA QUEM QUISER BAIXAR O REPOSITÓRIO E TESTAR LOCAL##

``Configurar o SQLite``

1 - Faça o  donwload em: https://www.sqlite.org/ para sua versão de bits do S.O.
2 - Instale em seus disco local C:
3- Configure nas variáveis de ambiente, a variável PATH adicionando uma nova variável com o caminho do executável do sqlite que foi salvo no C:

# String de conexão no appsettings.json

"ConnectionStrings": {
  "DefaultConnection": "Data Source=C:\\SQLite\\mydatabase.db"
}

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

