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

1. Register a new user

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
## Entregáveis:
   1. **Código Fonte:**
      - Código fonte do projeto, organizado conforme a estrutura acima.
   2. **Repositório GitHub:**
      - Repositório público contendo o código fonte e documentação.
   3. **Documentação:**
      - README.md com instruções sobre como configurar e executar o projeto, além de detalhes dos endpoints da API.

### Detalhes Técnicos: 🔧
- **Boas Práticas:** Utilizar boas práticas de código limpo, legível e bem documentado.
- **Git:** Utilizar Git para controle de versão e submeter o projeto através de um repositório público no GitHub.

### Dicas para Abordar o Projeto 🌟
- **Crie um Fork desse Repositório.**
- **Criar do Zero:** É fundamental que o projeto seja desenvolvido completamente do zero, demonstrando suas habilidades e criatividade desde o início.
- **Utilize bibliotecas** como Express para criação da API e jsonwebtoken para autenticação.
- **Documente cada etapa do processo para facilitar a compreensão.**

### Critérios de Avaliação: 📝
- **Funcionalidade:** A aplicação atende aos requisitos funcionais e funciona corretamente?
- **Qualidade do Código:** O código é limpo, bem estruturado e adequadamente documentado?
- **Segurança:** A autenticação foi implementada corretamente e as rotas estão protegidas?
- **Uso do Git:** O controle de versão é usado de forma eficaz com mensagens de commit significativas?
- **Documentação:** A documentação é clara e detalha o processo de desenvolvimento e uso da API?

### Não Queremos 🚫
- Descobrir que o candidato não foi quem realizou o teste.
- Ver commits grandes sem muita explicação nas mensagens no repositório.
- Entregas padrão ou cópias de outros projetos. Buscamos originalidade e autenticidade em cada contribuição.

### Prazo ⏳
A data máxima para entrega das trilhas foi removida, permitindo que as pessoas entreguem conforme sua disponibilidade. No entanto, ainda é necessário concluir a trilha com sucesso para ser inserido em uma equipe.

### Instruções de Entrega: 📬
Após finalizar o projeto, publique-o em uma URL pública (por exemplo, Vercel, Netlify, GitHub Pages, etc.) e hospede o seu servidor na nuvem. Use serviços que ofereçam uso gratiuto por um período, como a AWS e preencha o [Formulário](https://forms.gle/gZViPMTSDV5nidSu6):  

---

### Desafio da Inovação 🚀
Achou esse projeto inicial simples? Eleve ainda mais! Estamos em busca de mentes inovadoras que não apenas criem, mas que também desafiem os padrões. Como você pode transformar essa estrutura inicial em algo verdadeiramente extraordinário? Demonstre o poder da sua criatividade e o impacto das suas ideias inovadoras!

---

🔗 **Mantenha-se Conectado:**
- [Discord](https://discord.gg/wzA9FGZHNv)
- [Website](http://www.codigocertocoders.com.br/)
- [LinkedIn](https://www.linkedin.com/company/codigocerto/)
  
🌐 **Contato:**
- Email: codigocertocoders@gmail.com

---

### Precisa de Ajuda?
Está com alguma dificuldade, encontrou algum problema no desafio ou tem alguma sugestão pra gente? Crie uma issue e descreva o que achar necessário.

**Construindo o amanhã, hoje.**
