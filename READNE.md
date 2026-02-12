📌 ModuloAPI

API REST simples em ASP.NET Core para gerenciar Contatos, criada como projeto de estudo de backend utilizando C#, Entity Framework Core e SQLite (ou outro provider de banco).
Esse projeto inclui funcionalidades básicas de CRUD (Create, Read, Update, Delete) para o recurso Contato.

🧠 Funcionalidades

A API fornece endpoints para:

✅ Criar um novo contato

✅ Listar todos os contatos

✅ Buscar um contato por ID

✅ Atualizar um contato existente

✅ Deletar um contato

🗂️ Estrutura do Projeto

O código está organizado em pastas conforme boas práticas:
```TEXT
Pasta	Descrição
Controllers	Controladores da API (Endpoints REST)
Entities	Classes modelo que representam as tabelas do banco
DTOs	Objetos de transferência de dados
Repository	Implementação do padrão Repository
Context	Configuração do DbContext (EF Core)
Migrations	Migrations para criar o banco
DB	Scripts de configuração e banco local
Properties	Configurações gerais do projeto
```

🚀 Tecnologias utilizadas
Este projeto usa:

🟢 C#
📦 ASP.NET Core Web API
🗄️ Entity Framework Core
🧱 SQLite (opcional)
📄 Swagger (para documentação interativa)

🎯 Requisitos

Para rodar localmente, você precisa:
✔ .NET SDK instalado (versão compatível com o projeto)
✔ Editor de código (ex: VS Code ou Visual Studio)
✔ SQLite ou outro banco compatível

💻 Como executar localmente
1. Clone o repositório
```sh
git clone https://github.com/Jean5316/ModuloAPI.git
```
2. Acesse a pasta do projeto
```sh
cd ModuloAPI
```
3. Restaurar dependências
```sh
dotnet restore
```
4. Aplicar migrations e gerar banco
```sh
dotnet ef database update
```
5. Rodar a aplicação
```sh
dotnet run
```

Ou com hot-reload:
```sh
dotnet watch run
```

📌 Endpoints principais
```TEXT
Método	Endpoint	Ação
GET	/Contatos	Lista todos os contatos
GET	/Contatos/{id}	Busca um contato por ID
POST	/Contatos	Cria um novo contato
PUT	/Contatos/{id}	Atualiza um contato
DELETE	/Contatos/{id}	Deleta um contato
(Os nomes podem variar conforme rotas definidas no Controller)
```

📝 Exemplos de uso
Você pode testar a API usando o Swagger (se configurado) ou ferramentas como:
🛠️ Postman | Insomnia | curl
📌 Swagger (Documentação Interativa)
Se o projeto estiver com Swagger habilitado, após rodar:

👉 Acesse no navegador:
```bash
http://localhost:5000/swagger
```
(ou outra porta onde sua API estiver rodando)

🧾 Licença

Este projeto é aberto e pode ser usado livremente.

💬 Contribuições

Fique à vontade para melhorar, abrir issues ou enviar pull requests! 🚀